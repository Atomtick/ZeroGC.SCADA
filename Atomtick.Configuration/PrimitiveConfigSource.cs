using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Atomtick.Common;
using Microsoft.Data.Sqlite;
using SCADA.Common;
#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace Atomtick.Configuration
{
    public delegate bool AppendValidationRule(string config, string value, PrimitiveConfigSource configSource);

    public delegate string[] CustomizeOptions(string config, PrimitiveConfigSource configSource);

    public partial class PrimitiveConfigSource : IDisposable
    {
        private readonly Channel<ListDict> _channel = Channel.CreateUnbounded<ListDict>(new UnboundedChannelOptions() { SingleReader = true });

        private readonly string _dbConnectionString;
        private readonly Task _saveTask;

        private readonly IDictionary<string, ConfigItem> _configItems = new Dictionary<string, ConfigItem>();

        private bool _disposed;

        private bool _hasTable_config_current_value = false;
        private bool _hasTable_config_history_value = false;
        private bool _hasTable_config_schema_document = false;

        private const string CONFIG_CURRENT_VALUE = "config_current_value";
        private const string CONFIG_HISTORY_VALUE = "config_history_value";
        private const string CONFIG_SCHEMA_DOCUMENT = "config_schema_document";

        // 实例化一个顺序锁
        private readonly SeqLock _seqLock = new SeqLock();

        public PrimitiveConfigSource(string sqliteDB, ConfigSettings settings = null)
        {
            if (!File.Exists(sqliteDB))
                throw new FileNotFoundException("The sqlite DB file does not exist.", sqliteDB);
            Settings = settings ?? new ConfigSettings();
            _dbConnectionString = $"Data Source={sqliteDB}";
            _transactionBuffer = new ConcurrentDictionary<long, ListDict>();
            Initialize();
#if NET8_0_OR_GREATER
            _configItems = _configItems.ToFrozenDictionary();
#endif
            _saveTask = Function();
        }

        public event Action<ListDict> ValueChanged;

        public ConfigSettings Settings { get; }

        public void Dispose()
        {
            if (!_disposed)
            {
                _channel.Writer.Complete();
                _saveTask?.Wait(); // 等待保存任务完成
                // GC.SuppressFinalize(this); // 不是必须的，因为没析构函数
                _disposed = true;
            }
        }

        private async Task Function()
        {
            try
            {
                var asyncEnumerator = _channel.Reader.ReadAllAsync().GetAsyncEnumerator();
                try
                {
                    while (await asyncEnumerator.MoveNextAsync().ConfigureAwait(false))
                    {
                        var pairs = asyncEnumerator.Current;
                        if (pairs.Any())
                        {
                            SaveToSqlite(pairs);
                        }
                    }
                }
                finally
                {
                    if (asyncEnumerator != null)
                    {
                        await asyncEnumerator.DisposeAsync().ConfigureAwait(false);
                    }
                }
            }
            catch { }
        }

        private void SaveToSqlite(ListDict changedItems)
        {
            if (!Settings.RestoreOnAppStartup)
            {
                using (var connection = new SqliteConnection(_dbConnectionString))
                {
                    connection.Open();
                    // 开启事务
                    using (var transaction = connection.BeginTransaction())
                    {
                        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        var command = connection.CreateCommand();
                        command.CommandText =
                            $"INSERT INTO {CONFIG_CURRENT_VALUE} (name, value, time) \nVALUES ($name, $value,$time )\nON CONFLICT(name) DO UPDATE SET \n    value = excluded.value,\n\ttime = excluded.time;";
                        var paramName = command.Parameters.AddWithValue("$name", "");
                        var paramValue = command.Parameters.AddWithValue("$value", "");
                        var paramTime = command.Parameters.AddWithValue("$time", "");

                        foreach (var changedItem in changedItems)
                        {
                            paramName.Value = changedItem.Key;
                            paramValue.Value = changedItem.Value as string;
                            paramTime.Value = timestamp;
                            command.ExecuteNonQuery();
                        }
                        // 提交事务（这个时候才真正执行一次集中的磁盘 I/O）
                        transaction.Commit();
                    }
                    if (Settings.IsConfigModificationTrackingEnabled)
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            var command = connection.CreateCommand();
                            command.CommandText = $"INSERT INTO {CONFIG_HISTORY_VALUE} (name,new_value,\"time\") VALUES($name,$new_value,$time);";
                            var paramName = command.Parameters.AddWithValue("$name", "");
                            var paramValue = command.Parameters.AddWithValue("$new_value", "");
                            var paramTime = command.Parameters.AddWithValue("$time", "");

                            foreach (var changedItem in changedItems)
                            {
                                paramName.Value = changedItem.Key;
                                paramValue.Value = changedItem.Value as string;
                                paramTime.Value = timestamp;
                                command.ExecuteNonQuery();
                            }
                            // 提交事务（这个时候才真正执行一次集中的磁盘 I/O）
                            transaction.Commit();
                        }
                    }
                }
            }
        }
    }
}
