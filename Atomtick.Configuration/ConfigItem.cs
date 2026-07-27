using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using SCADA.Common;
using SCADA.Common.Interfaces;

namespace Atomtick.Configuration
{
    public class ConfigItem : ICloneable
    {
        private static readonly ConcurrentDictionary<string, ConfigItem> _absentConfigCache = new ConcurrentDictionary<string, ConfigItem>();

        public string Description { get; internal set; }

        public string Display { get; internal set; }

        public bool Enable { get; internal set; }

        public string MaxValue { get; internal set; }

        public string MinValue { get; internal set; }

        public string Name { get; internal set; }

        public string Path { get; internal set; }

        public object ObjectValue { get; internal set; }

        public IReadOnlyList<string> Options { get; internal set; }

        public string Regex { get; internal set; }

        public string RegexNote { get; internal set; }

        public bool Restart { get; internal set; }

        public string StringValue { get; internal set; }

        public ConfigType Type { get; internal set; }

        public string Unit { get; internal set; }

        public Action<string> ValidationRule { get; internal set; }

        public bool Visible { get; internal set; }

        object ICloneable.Clone()
        {
            var copy = new ConfigItem()
            {
                Name = Name,
                Type = Type,
                Display = Display,
                Description = Description,
                StringValue = StringValue,
                ObjectValue = ObjectValue,
                Regex = Regex,
                RegexNote = RegexNote,
                Unit = Unit,
                Restart = Restart,
                Visible = Visible,
                MaxValue = MaxValue,
                MinValue = MinValue,
                Enable = Enable,
                Options = Options,
            };
            // 引用类型需要深拷贝,值类型和不可变引用类型(string)的等号赋值本身就是深拷贝
            if (ObjectValue is FileInfo fileInfo)
            {
                copy.ObjectValue = new FileInfo(fileInfo.FullName);
            }
            else if (ObjectValue is DirectoryInfo directoryInfo)
            {
                copy.ObjectValue = new FileInfo(directoryInfo.FullName);
            }
            else
            {
                copy.ObjectValue = ObjectValue;
            }
            return copy;
        }

        // 是否含有此配置
        public bool IsAbsent()
        {
            return StringValue == null && ObjectValue == null && Type == ConfigType.Unknown;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(nameof(Name) + ":" + Name ?? "NULL");
            sb.AppendLine(nameof(StringValue) + ":" + StringValue ?? "NULL");
            sb.AppendLine(nameof(Type) + ":" + Type ?? "NULL");
            sb.AppendLine(nameof(Description) + ":" + Description ?? "NULL");
            sb.AppendLine(nameof(MinValue) + ":" + MinValue ?? "NULL");
            sb.AppendLine(nameof(MaxValue) + ":" + MaxValue ?? "NULL");
            sb.AppendLine(nameof(Options) + ":" + Options ?? "NULL");
            sb.AppendLine(nameof(Regex) + ":" + Regex ?? "NULL");
            sb.AppendLine(nameof(RegexNote) + ":" + RegexNote ?? "NULL");
            sb.AppendLine(nameof(Enable) + ":" + Enable ?? "NULL");
            sb.AppendLine(nameof(Restart) + ":" + Restart ?? "NULL");
            sb.AppendLine(nameof(Visible) + ":" + Visible ?? "NULL");
            return sb.ToString();
        }

        public string ToString(object @object)
        {
            if (@object == null)
            {
                throw new ArgumentException($"Config item '{Name}' is absent.");
            }
            return StringValue;
        }

        public string ToString(object @object, string defaultValue)
        {
            return @object == null ? defaultValue : StringValue;
        }

        public bool TryToString(object @object, out string @string)
        {
            if (@object == null)
            {
                @string = default;
                return false;
            }
            @string = StringValue;
            return true;
        }

        internal static ConfigItem CreteAbsent(string name)
        {
            return string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Config name can't be null or empty")
                : _absentConfigCache.GetOrAdd(
                    name,
                    _ => new ConfigItem()
                    {
                        Name = name,
                        Path = name,
                        ObjectValue = null,
                        StringValue = null,
                        Type = ConfigType.Unknown,
                    }
                );
        }
    }
}
