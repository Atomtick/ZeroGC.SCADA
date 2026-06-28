using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource
    {
        private string[] CheckConfigItemFormatting(string config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config = config.Trim();
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentException("config can not be white space.", nameof(config));
            }

            var names = config.Split('.');
            if (config.StartsWith(".") || config.EndsWith(".") || names.Length < 2 || names.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException($"There must be at least one dot in the middle of the {config}, and it cannot appear at both ends, and it cannot appear consecutively", nameof(config));
            }

            return names;
        }

        private void CheckConfigFormattingValid(string config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config = config.Trim();
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentException("config can not be white space.", nameof(config));
            }

            if (config.StartsWith(".") || config.EndsWith(".") || config.Contains('.') == false || config.Contains(".."))
            {
                throw new ArgumentException($"Config '{config}' formatting is invalid.", nameof(config));
            }
        }

        private object Convert2Object(ConfigType type, string value)
        {
            switch (type)
            {
                case ConfigType.Bool:
                    return bool.Parse(value);

                case ConfigType.Integer:
                    StringParser.TryParse2Int64(value, out long @long);
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
    }
}
