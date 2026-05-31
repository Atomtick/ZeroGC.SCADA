using SCADA.Common;
using SCADA.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace SCADA.Configuration
{
    // 要支持以字符串方式读取配置值，目的是UI可查询，SECS-GEM亦可查询。
    // Save时,考虑使用AppendLine优化,既不会产生大于85KB的大对象,也不会产生几万个小片段字符串.
    public partial class PrimitiveConfigSource : IDisposable
    {
   
        private readonly Channel<IEnumerable<(string configItem, string value)>> _channel = Channel.CreateUnbounded<IEnumerable<(string configItem, string value)>>();

        private readonly Task _saveTask;

        private readonly string _xmlFilePath;

        private volatile IDictionary<string, ConfigItem> _configItems;

        private bool _disposed = false;

        // 实例化一个顺序锁
        private SequenceLock _seqLock = new SequenceLock();

        private readonly string _dbConnectionString;
        readonly bool _supportAtomicOperations;
        readonly bool _trackConfigValueModification;
        readonly bool _restoreOnAppStartup;

        public PrimitiveConfigSource(string sqliteDB, bool supportAtomicOperations, bool trackConfigValueModification, bool restoreOnAppStartup = false)
        {
            _dbConnectionString = $"Data Source={sqliteDB};Version=3;";
            _supportAtomicOperations = supportAtomicOperations;
            _trackConfigValueModification = trackConfigValueModification;
            _restoreOnAppStartup = restoreOnAppStartup;
        }

        public PrimitiveConfigSource(string xmlFile, bool supportAtomicOperations)
        {
            _dbConnectionString = xmlFile;
            _supportAtomicOperations= supportAtomicOperations;
            _trackConfigValueModification= false;
            _restoreOnAppStartup = true;
        }

        public PrimitiveConfigSource(string xmlFilePath, bool restoredEachTimeRestartingApplication,int capacity)
        {
            foreach (var item in _configItems)
            {
                ValidateValue(item.Key, item.Value.StringValue);
            }

            async Task Function()
            {
                try
                {
                    var asyncEnumerator = _channel.Reader.ReadAllAsync().GetAsyncEnumerator();
                    try
                    {
                        while (await asyncEnumerator.MoveNextAsync().ConfigureAwait(false))
                        {
                            var pairs = asyncEnumerator.Current;
                            if (pairs.Count() > 0)
                            {
                                if (!_restoreOnAppStartup)
                                {
                                    Save(pairs);
                                }
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

            _saveTask = Function();
        }

        public event Action<IEnumerable<(string config, string oldValue, string newValue)>> ValueSet;

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

        protected virtual string[] CustomizeOptionsSource(IConfigSourceReader configSource, string config)
        {
            return null;
        }

        protected virtual Func<string, bool> CustomizeValidationRule(IConfigSourceReader configSource, string config)
        {
            return null;
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
            if (config.StartsWith(".") || config.EndsWith(".") || names.Length < 2 || names.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException($"There must be at least one dot in the middle of the {config}, and it cannot appear at both ends, and it cannot appear consecutively", nameof(config));
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
        private void WriteConfigValues(LightWeightDictionary keyValuePairs)
        {

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

        // 正常情况下看到的文件是：app.config(新) app.config.bk(旧)
        // Replace失败的话看到的文件是： app.config.tmp.bk(新) app.config(旧) app.config.bk(更旧)
        private void Save(IEnumerable<(string config, string value)> changedItems)
        {
            if (changedItems != null && changedItems.Count() != 0)
            {
                foreach (var item in changedItems)
                {
                    foreach (var node in RootNodes)
                    {
                        GetConfigNode(item.config).ConfigItems.First(x => x.Name == item.config.Substring(item.config.LastIndexOf('.'))).StringValue = item.value;
                    }
                }

                string fileFolder = Path.GetDirectoryName(_xmlFilePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_xmlFilePath);
                string extension = Path.GetExtension(_xmlFilePath);
                string bkFileName = fileNameWithoutExtension + ".bk" + extension;
                string bkFilePath = Path.Combine(fileFolder, bkFileName);
                string tmpFileName = fileNameWithoutExtension + ".tmp.bk" + extension;
                string tmpFilePath = Path.Combine(fileFolder, tmpFileName);

                using (FileStream fs = new FileStream(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192))
                {
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true,                      // 是否需要缩进（按需开启，会稍微增加文件大小）
                        CloseOutput = false, // 让 using 块自己处理 FileStream 的关闭
                    };
                    // 3. 将 XmlWriter 绑定到 FileStream
                    using (XmlWriter writer = XmlWriter.Create(fs, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("root");

                        // 4. 循环生成节点，边写边刷入磁盘，不保留任何大集合
                        foreach (var node in RootNodes)
                        {
                            Write(writer, node);
                        }

                        writer.WriteEndElement(); // 结束 <root>
                        writer.WriteEndDocument();

                        // writer 和 fs 的 using 块结束时会自动调用 Flush() 和 Dispose()
                        // 这会将剩余的少量缓冲数据写入磁盘。
                    }
                }
                File.Replace(tmpFilePath, _xmlFilePath, bkFilePath);

                void Write(XmlWriter writer, ConfigNode node)
                {
                    writer.WriteStartElement("config");
                    writer.WriteAttributeString("name", node.Name);
                    if (!string.IsNullOrWhiteSpace(node.Display))
                        writer.WriteAttributeString("display", node.Display);
                    if (node.Visible == false)
                        writer.WriteAttributeString("visible", node.Visible ? "true" : "false");
                    if (node.Enable == false)
                        writer.WriteAttributeString("enable", node.Enable ? "true" : "false");

                    foreach (var item in node.ConfigItems)
                    {
                        writer.WriteElementString("config", null);
                        writer.WriteAttributeString("value", item.StringValue);
                        writer.WriteAttributeString("name", item.Name);
                        writer.WriteAttributeString("type", item.Type.ToString());
                        if (!string.IsNullOrWhiteSpace(item.MinValue))
                            writer.WriteAttributeString("min", item.MinValue);
                        if (!string.IsNullOrWhiteSpace(item.MaxValue))
                            writer.WriteAttributeString("max", item.MaxValue);
                        if (item.Options != null && item.Options.Length > 0)
                            writer.WriteAttributeString("options", string.Join(";", item.Options));
                        if (!string.IsNullOrWhiteSpace(item.Regex))
                            writer.WriteAttributeString("regex", item.Regex);
                        if (!string.IsNullOrWhiteSpace(item.RegexNote))
                            writer.WriteAttributeString("regexnote", item.RegexNote);
                        if (!string.IsNullOrWhiteSpace(item.Unit))
                            writer.WriteAttributeString("unit", item.Unit);
                        if (!string.IsNullOrWhiteSpace(item.Display))
                            writer.WriteAttributeString("display", item.Display);
                        if (!string.IsNullOrWhiteSpace(item.Description))
                            writer.WriteAttributeString("desc", item.Description);
                        if (item.Restart == true)
                            writer.WriteAttributeString("restart", item.Restart ? "true" : "false");
                        if (node.Visible == false)
                            writer.WriteAttributeString("visible", item.Visible ? "true" : "false");
                        if (node.Enable == false)
                            writer.WriteAttributeString("enable", item.Enable ? "true" : "false");
                    }

                    if (node.Children != null && node.Children.Count > 0)
                    {
                        foreach (var child in node.Children)
                        {
                            Write(writer, child);
                        }
                    }
                    else
                    {
                        writer.WriteEndElement();
                    }
                }
            }
        }
    }
}