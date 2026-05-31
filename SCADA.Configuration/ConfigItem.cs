using SCADA.Common;
using SCADA.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SCADA.Configuration
{
    public partial class ConfigItem : ICloneable,IConfigItem
    {
        private static readonly ConcurrentDictionary<string, ConfigItem> _absentConfigCache = new ConcurrentDictionary<string, ConfigItem>();

        public string Description
        {
            get; set;
        }

        public string Display
        {
            get; set;
        }

        public bool Enable { get; set; }

        public string MaxValue
        {
            get; set;
        }

        public string MinValue
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public object ObjectValue { get; set; }

        public string[] Options
        {
            get; set;
        }

        public string Regex
        {
            get; set;
        }

        public string RegexNote { get; set; }

        public bool Restart
        {
            get; set;
        }

        /// <summary>
        /// ECID
        /// </summary>
        public int SlotIndex { get; set; }

        public string StringValue
        {
            get; set;
        }

        public ConfigType Type
        {
            get; set;
        }

        public string Unit
        {
            get; set;
        }

        public Action<string> ValidationRule { get; set; }

        public bool Visible
        {
            get; set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowConfigNotFoundException(string config)
        {
            throw new KeyNotFoundException($"Config item '{config}' doesn't exist.");
        }

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
            return StringValue == null && ObjectValue == null && SlotIndex == -1;
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
                @string = default; return false;
            }
            @string = StringValue; return true;
        }

        internal static ConfigItem CreteAbsent(string name)
        {
            return string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Config name can't be null or empty")
                : _absentConfigCache.GetOrAdd(name, _ => new ConfigItem()
                {
                    Name = name,
                    ObjectValue = null,
                    StringValue = null,
                });
        }
    }
}