using SCADA.Common;
using SCADA.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Sqlite;

namespace SCADA.Configuration
{
    public delegate string[] CustomizeOptions(string config, PrimitiveConfigSource configSource);

    public delegate bool AppendValidationRule(string config, string value, PrimitiveConfigSource configSource);

    public partial class PrimitiveConfigSource : IDisposable
    {
        private readonly Channel<LightWeightDictionary> _channel = Channel.CreateUnbounded<LightWeightDictionary>();

        private readonly Task _saveTask;

        private readonly string _xmlFilePath;

        private volatile IDictionary<string, ConfigItem> _configItems;

        private bool _disposed;

        // 实例化一个顺序锁
        private SequenceLock _seqLock = new SequenceLock();

        private readonly string _dbConnectionString;

        public ConfigSourceSettings Settings { get; }

        public PrimitiveConfigSource(string sqliteDB, ConfigSourceSettings settings = null)
        {
            if (!File.Exists(sqliteDB))
                throw new FileNotFoundException("The sqlite DB file does not exist.", sqliteDB);
            Settings = settings ?? new ConfigSourceSettings();
            _dbConnectionString = $"Data Source={sqliteDB};Version=3;";
            LoadSqlite();
            _saveTask = Function();
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
            catch
            {
            }
        }

        public event Action<LightWeightDictionary> ValueSet;

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

        private string[] CheckConfigItemFormatting(string config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config = config.Trim();
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentException("ConfigItem cannot be white space.", nameof(config));
            }

            var names = config.Split('.');
            if (config.StartsWith(".") || config.EndsWith(".") || names.Length < 2 ||
                names.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException(
                    $"There must be at least one dot in the middle of the {config}, and it cannot appear at both ends, and it cannot appear consecutively",
                    nameof(config));
            }

            return names;
        }

        private object Convert2Object(ConfigType type, string value)
        {
            switch (type)
            {
                case ConfigType.Bool:
                    return bool.Parse(value);

                case ConfigType.Integer:
                    StringParser.TryParse2Long(value, out long @long);
                    return @long;

                case ConfigType.Decimal:
                    StringParser.TryParse2Double(value, out double @double);
                    return @double;

                case ConfigType.String:
                    return value;

                case ConfigType.File:
                    StringParser.TryParse2File(value, out var file);
                    return file;

                case ConfigType.Folder:
                    StringParser.TryParse2Directory(value, out var folder);
                    return folder;

                case ConfigType.Color:
                    StringParser.TryParse2Color(value, out var color);
                    return color;

                case ConfigType.DateTime:
                    StringParser.TryParse2DateTime(value, out var dateTime);
                    return dateTime;

                default:
                    return null;
            }
        }

        private string Convert2String(object value)
        {
            if (value is DateTime dateTime)
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            if (value is System.Drawing.Color color)
                return "#" + color.ToArgb().ToString("X8", CultureInfo.InvariantCulture);

            if (value is FileInfo fileInfo)
                return fileInfo.FullName;

            if (value is DirectoryInfo directoryInfo)
                return directoryInfo.FullName;

            return value.ToString();
        }

        private bool _hasTable_config_current_value = false;

        private void SaveToSqlite(LightWeightDictionary changedItems)
        {
            if (!Settings.RestoreOnAppStartup)
            {
                if (!_hasTable_config_current_value)
                {
                    string hasTableSql =
                        "SELECT count(*) as has_table\nFROM sqlite_master \nWHERE type = 'table' AND name = 'config_current_value';";
                    using (var connection = new SqliteConnection(_dbConnectionString))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = hasTableSql;
                            var hasTable = command.ExecuteNonQuery();
                            if (hasTable == 0)
                            {
                                string createTableSql =
                                    "CREATE TABLE \"config_current_value\" (\n\t\"name\"\tTEXT,\n\t\"value\"\tTEXT NOT NULL,\n\t\"time\"\tINTEGER NOT NULL,\n\tPRIMARY KEY(\"name\")\n)";
                                using (var command2 = connection.CreateCommand())
                                {
                                    command2.CommandText = createTableSql;
                                    command2.ExecuteNonQuery();
                                }

                                _hasTable_config_current_value = true;
                            }
                        }
                    }
                }

                using (var connection = new SqliteConnection(_dbConnectionString))
                {
                    connection.Open();
                    // 开启事务
                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = connection.CreateCommand();
                        command.CommandText =
                            "INSERT INTO config_current_value (name, value, time) \nVALUES ($name, $value,$time )\nON CONFLICT(name) DO UPDATE SET \n    value = excluded.value,\n\ttime = excluded.time;";
                        var paramName = command.Parameters.AddWithValue("$name", "");
                        var paramValue = command.Parameters.AddWithValue("$value", "");
                        var paramTime = command.Parameters.AddWithValue("$time", "");

                        foreach (var changedItem in changedItems)
                        {
                            paramName.Value = changedItem.Key;
                            paramValue.Value = changedItem.Value as string;
                            paramTime.Value = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            command.ExecuteNonQuery();
                        }
                        // 提交事务（这个时候才真正执行一次集中的磁盘 I/O）
                        transaction.Commit();
                    }
                }
            }

            if (Settings.TrackConfigValueModification)
            {
            }
        }
    }
}