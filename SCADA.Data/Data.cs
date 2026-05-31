using Npgsql;
using SCADA.Common.Interfaces;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SCADA.Data
{
    public class Data : IDisposable
    {
        private const string _sql = "COPY metrics (time, name, value_double, value_text, value_type) FROM STDIN (FORMAT BINARY)";
        private const string DAQDatabaseConnectionStringConfig = "System.Data.DAQDatabaseConnectionString";
        private const string DAQMaxQueueSizeConfig = "System.Data.DAQMaxQueueSize";
        private const string EnableDAQConfig = "System.Data.EnableDAQ";
        private const string EnableUIPollingConfig = "System.Data.EnableUIPolling";
        private readonly Channel<DataPoint> _channel; // 直接流转 DataPoint
        private readonly IConfigSourceReader _configSource;
        private readonly NpgsqlConnection _conn;
        private readonly string _connectionString;
        private readonly Task _consumerTask;
        private readonly CancellationTokenSource _cts;
        private readonly Dictionary<string, DataItem> _dataItems;
        private readonly ConcurrentDictionary<string, double?> _lastDoubleValueDict = new ConcurrentDictionary<string, double?>();
        private readonly ConcurrentDictionary<string, string> _lastStringValueDict = new ConcurrentDictionary<string, string>();
        private readonly int _maxBatchSize = 5000;
        private string _daqDatabaseConnectionString;
        private int _daqMaxQueueSize;
        private bool _disposedValue;
        private bool _enableDAQ;
        private bool _enableUIPolling;

        public Data(IConfigSourceReader configSource)
        {
            _configSource = configSource;

            (IConfigItem enableDAQ, IConfigItem enableUIPolling, IConfigItem daqDatabaseConnectionString, IConfigItem daqMaxQueueSize)
                = _configSource.Get(EnableDAQConfig, EnableUIPollingConfig, DAQDatabaseConnectionStringConfig, DAQMaxQueueSizeConfig);

            _enableDAQ = enableDAQ.To(true);
            _enableUIPolling = enableUIPolling.To(true);
            _daqMaxQueueSize = daqMaxQueueSize.To(100000);
            _daqDatabaseConnectionString = daqDatabaseConnectionString.To<string>();

            _cts = new CancellationTokenSource();
            _dataItems = new Dictionary<string, DataItem>();
            _conn = new NpgsqlConnection(_daqDatabaseConnectionString);
            var options = new BoundedChannelOptions(_daqMaxQueueSize)
            {
                FullMode = BoundedChannelFullMode.DropWrite,
                SingleReader = true,
                SingleWriter = false
            };
            // Channel 现在直接接收结构体
            _channel = Channel.CreateBounded<DataPoint>(options);

            _consumerTask = Task.Run(() => ConsumeAsync(_cts.Token));
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Record(string name, bool? value, long timestamp)
        {
            var point = new DataPoint
            {
                Timestamp = timestamp,
                Name = name,
                DataType = DataType.Bool,
                DoubleValue = value.HasValue ? value.Value ? 1 : 0 : null
            };
            _channel.Writer.TryWrite(point);
        }

        public void Record(string name, long? value, long timestamp)
        {
            var point = new DataPoint
            {
                Timestamp = timestamp,
                Name = name,
                DataType = DataType.Long,
                DoubleValue = value.HasValue ? (double)value.Value : null,
            };
            _channel.Writer.TryWrite(point);
        }

        public void Record(string name, double? value, long timestamp)
        {
            var point = new DataPoint
            {
                Timestamp = timestamp,
                Name = name,
                DataType = DataType.Double,
                DoubleValue = value,
            };
            _channel.Writer.TryWrite(point);
        }

        public void Record(string name, string value, long timestamp)
        {
            var point = new DataPoint
            {
                Timestamp = timestamp,
                Name = name,
                DataType = DataType.String512,
                StringValue = value
            };
            _channel.Writer.TryWrite(point);
        }

        //Dictionary和ConcurrentDictionary的性能差多少倍
        public void Register(string name, DataType dataType, DataPurpose dataPurpose = DataPurpose.DAQ_UI)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(name)} can't be null or white space.");
            if (_dataItems.TryAdd(name, new DataItem()
            {
                Name = name,
                DataItemType = dataType,
                DataItemPurpose = dataPurpose,
                Data = this
            }) == false)
            {
                throw new ArgumentException($"{name} already exists.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        /// <summary>
        /// 消费者：自适应零 GC 批处理核心逻辑
        /// </summary>
        private async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            // 消费者在内部维护一个长期存活的 Buffer，无需每次申请
            var buffer = ArrayPool<DataPoint>.Shared.Rent(_maxBatchSize);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int count = 0;

                    // 1. 阻塞等待：直到队列中有至少 1 条数据到来
                    buffer[count++] = await _channel.Reader.ReadAsync(cancellationToken);

                    // 2. 自适应聚合：只要缓冲没满，且队列里还有现成的数据，就疯狂拉取
                    // TryRead 是同步且非阻塞的，极度高效
                    while (count < _maxBatchSize && _channel.Reader.TryRead(out var point))
                    {
                        buffer[count++] = point;
                    }

                    // 3. 写入数据库
                    // 高峰期：count 会瞬间飙到 5000，实现大批量写入
                    // 低谷期：count 可能是 1，立刻写入，保证低延迟
                    try
                    {
                        await WriteToTimescaleDbAsync(buffer, count, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"DB Write Failed: {ex.Message}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常停机引发的取消
            }
            finally
            {
                // 归还内部缓冲
                ArrayPool<DataPoint>.Shared.Return(buffer, clearArray: true);
            }
        }

        // 消费者写入逻辑改造：
        private async Task WriteToTimescaleDbAsync(DataPoint[] buffer, int count, CancellationToken cancellationToken)
        {
            if (_conn.State != System.Data.ConnectionState.Open)
                await _conn.OpenAsync(cancellationToken);

            // 对应表结构的 4 个列
            using var writer = await _conn.BeginBinaryImportAsync(_sql
                , cancellationToken);

            for (int i = 0; i < count; i++)
            {
                await writer.StartRowAsync(cancellationToken);
                await writer.WriteAsync(DateTimeOffset.FromUnixTimeMilliseconds(buffer[i].Timestamp), NpgsqlTypes.NpgsqlDbType.TimestampTz, cancellationToken);
                await writer.WriteAsync(buffer[i].Name, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);

                // 根据 DataType 分流写入
                if (buffer[i].DataType == DataType.Number)
                {
                    await writer.WriteAsync(buffer[i].DoubleValue, NpgsqlTypes.NpgsqlDbType.Double, cancellationToken);
                    await writer.WriteAsync(DBNull.Value, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken); // 必须显式写 NULL
                }
                else
                {
                    await writer.WriteAsync(DBNull.Value, NpgsqlTypes.NpgsqlDbType.Double, cancellationToken); // 必须显式写 NULL
                    await writer.WriteAsync(buffer[i].StringValue, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);
                }
            }

            await writer.CompleteAsync(cancellationToken);
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DAQ()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
    }
}