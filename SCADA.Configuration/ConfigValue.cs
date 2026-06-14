using System;
using System.Drawing;
using System.IO;
using SCADA.Common;
using SCADA.Common.Interfaces;

namespace SCADA.Configuration
{
    public readonly struct ConfigValue
    {
        // 配置项object形式的值
        private readonly object @object;

        // 配置项字符串形式的值
        private readonly string @string;

        // 配置项名称
        private readonly string path;

        // 配置项的值类型
        private readonly ConfigType type;

        public ConfigValue(object @object, string @string, string path, ConfigType type)
        {
            this.@object = @object;
            this.@string = @string;
            this.path = path;
            this.type = type;
        }

        public bool IsAbsent()
        {
            return type == ConfigType.Unknown || @object == null || @string == null;
        }

        #region ToException

        public bool ToBool()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Bool)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Bool}'."
                );
            }
            if (@object is bool @bool)
            {
                return @bool;
            }
            throw new ApplicationException();
        }

        public Color ToColor()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Color)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Color}'."
                );
            }

            if (@object is Color color)
            {
                return color;
            }
            throw new ApplicationException();
        }

        public DateTime ToDateTime()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.DateTime)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.DateTime}'."
                );
            }
            if (@object is DateTime dateTime)
            {
                return dateTime;
            }

            throw new ApplicationException();
        }

        public DirectoryInfo ToDirectory()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }

            if (type != ConfigType.Folder)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Folder}'."
                );
            }

            if (@object is DirectoryInfo folder)
            {
                return folder;
            }

            throw new ApplicationException();
        }

        public double ToDouble()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Decimal)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Decimal}'."
                );
            }
            if (@object is double @double)
            {
                return @double;
            }

            throw new ApplicationException();
        }

        public FileInfo ToFile()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }

            if (type != ConfigType.File)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.File}'."
                );
            }

            if (@object is FileInfo fileInfo)
            {
                return fileInfo;
            }

            throw new ApplicationException();
        }

        public short ToInt16()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }

            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out short result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public int ToInt32()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out int result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public long ToInt64()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return @long;
            }

            throw new ApplicationException();
        }

        public sbyte ToInt8()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }

            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out sbyte result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public float ToSingle()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Decimal)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Decimal}'."
                );
            }
            if (@object is double @double)
            {
                return NumericConverter.TryConvert(
                    @double,
                    out float result,
                    ConversionRule.CheckOverflow | ConversionRule.CheckPrecision
                )
                    ? result
                    : throw new ArgumentException(
                        $"Overflow or precision loss occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public new string ToString()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            return @string;
        }

        public ushort ToUInt16()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out ushort result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public uint ToUInt32()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out uint result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public ulong ToUInt64()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out ulong result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public byte ToUInt8()
        {
            if (IsAbsent())
            {
                throw new ArgumentException($"Config item '{path}' is absent.");
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }

            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out byte result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        #endregion ToException

        #region ToDefaultValue

        public bool ToBool(bool defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Bool)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Bool}'."
                );
            }
            if (@object is bool @bool)
            {
                return @bool;
            }
            throw new ApplicationException();
        }

        public Color ToColor(Color defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Color)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Color}'."
                );
            }

            if (@object is Color color)
            {
                return color;
            }

            throw new ApplicationException();
        }

        public DateTime ToDateTime(DateTime defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.DateTime)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.DateTime}'."
                );
            }

            if (@object is DateTime dateTime)
            {
                return dateTime;
            }

            throw new ApplicationException();
        }

        public DirectoryInfo ToDirectory(DirectoryInfo defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }

            if (type != ConfigType.Folder)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Folder}'."
                );
            }

            if (@object is DirectoryInfo folder)
            {
                return folder;
            }

            throw new ApplicationException();
        }

        public double ToDouble(double defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Decimal)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Decimal}'."
                );
            }

            if (@object is double @double)
            {
                return @double;
            }

            throw new ApplicationException();
        }

        public FileInfo ToFile(FileInfo defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }

            if (type != ConfigType.File)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.File}'."
                );
            }

            if (@object is FileInfo file)
            {
                return file;
            }

            throw new ApplicationException();
        }

        public short ToInt16(short defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }

            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out short result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public int ToInt32(int defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out int result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public long ToInt64(long defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return @long;
            }

            throw new ApplicationException();
        }

        public sbyte ToInt8(sbyte defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out sbyte result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public float ToSingle(float defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Decimal)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Decimal}'."
                );
            }
            if (@object is double @double)
            {
                return NumericConverter.TryConvert(
                    @double,
                    out float result,
                    ConversionRule.CheckOverflow | ConversionRule.CheckPrecision
                )
                    ? result
                    : throw new ArgumentException(
                        $"Overflow or precision loss occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public string ToString(string defaultValue)
        {
            return IsAbsent() ? defaultValue : @string;
        }

        public ushort ToUInt16(ushort defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out ushort result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }
            throw new ApplicationException();
        }

        public uint ToUInt32(uint defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out uint result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public ulong ToUInt64(ulong defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }

            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }
            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out ulong result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        public byte ToUInt8(byte defaultValue)
        {
            if (IsAbsent())
            {
                return defaultValue;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException(
                    $"Config item '{path}' type is not '{ConfigType.Integer}'."
                );
            }

            if (@object is long @long)
            {
                return NumericConverter.TryConvert(
                    @long,
                    out byte result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer Overflow occurred for Config item '{path}'."
                    );
            }

            throw new ApplicationException();
        }

        #endregion ToDefaultValue

        #region TryTo

        public bool TryToBool(out bool @bool)
        {
            if (IsAbsent())
            {
                @bool = default;
                return false;
            }

            if (type != ConfigType.Bool)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'bool'.");
            }

            if (@object is bool @bool2)
            {
                @bool = @bool2;
                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToColor(out Color color)
        {
            if (IsAbsent())
            {
                color = default;
                return false;
            }

            if (type != ConfigType.Color)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'color'.");
            }

            if (@object is Color color2)
            {
                color = color2;
                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToDateTime(out DateTime dateTime)
        {
            if (IsAbsent())
            {
                dateTime = default;
                return false;
            }

            if (type != ConfigType.DateTime)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'datetime'.");
            }

            if (@object is DateTime datetime2)
            {
                dateTime = datetime2;
                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToDirectory(out DirectoryInfo directoryInfo)
        {
            if (IsAbsent())
            {
                directoryInfo = default;
                return false;
            }

            if (type != ConfigType.Folder)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'folder'.");
            }

            if (@object is DirectoryInfo folder)
            {
                directoryInfo = folder;
                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToDouble(out double @double)
        {
            if (IsAbsent())
            {
                @double = default;
                return false;
            }

            if (type != ConfigType.Decimal)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'double'.");
            }

            if (@object is double double2)
            {
                @double = double2;
                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToFile(out FileInfo fileInfo)
        {
            if (IsAbsent())
            {
                fileInfo = default;
                return false;
            }

            if (type != ConfigType.File)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'file'.");
            }

            if (@object is FileInfo file)
            {
                fileInfo = file;
                return true;
            }
            throw new ApplicationException();
        }

        public bool TryToInt16(out short @short)
        {
            if (IsAbsent())
            {
                @short = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long @long)
            {
                @short = NumericConverter.TryConvert(
                    @long,
                    out short result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );

                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToInt32(out int @int)
        {
            if (IsAbsent())
            {
                @int = default;
                return false;
            }

            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long @long)
            {
                @int = NumericConverter.TryConvert(
                    @long,
                    out int result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );

                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToInt64(out long @long)
        {
            if (IsAbsent())
            {
                @long = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long long2)
            {
                @long = long2;
                return true;
            }

            throw new ApplicationException();
        }

        public bool TryToInt8(out sbyte @sbyte)
        {
            if (IsAbsent())
            {
                @sbyte = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long @long)
            {
                @sbyte = NumericConverter.TryConvert(
                    @long,
                    out sbyte result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );

                return true;
            }
            throw new ApplicationException();
        }

        public bool TryToSingle(out float @float)
        {
            if (IsAbsent())
            {
                @float = default;
                return false;
            }
            if (type != ConfigType.Decimal)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'double'.");
            }

            if (@object is double @double)
            {
                @float = NumericConverter.TryConvert(
                    @double,
                    out float result,
                    ConversionRule.CheckOverflow | ConversionRule.CheckPrecision
                )
                    ? result
                    : throw new ArgumentException(
                        $"Overflow or precision loss occurred for Config item '{path}'."
                    );
                return true;
            }
            throw new ApplicationException();
        }

        public bool TryToString(out string @string)
        {
            if (IsAbsent())
            {
                @string = default;
                return false;
            }
            @string = this.@string;
            return true;
        }

        public bool TryToUInt16(out ushort @ushort)
        {
            if (IsAbsent())
            {
                @ushort = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }
            if (@object is long @long)
            {
                @ushort = NumericConverter.TryConvert(
                    @long,
                    out ushort result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );
                return true;
            }
            throw new ApplicationException();
        }

        public bool TryToUInt32(out uint @uint)
        {
            if (IsAbsent())
            {
                @uint = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long @long)
            {
                @uint = NumericConverter.TryConvert(
                    @long,
                    out uint result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );
                return true;
            }
            throw new ApplicationException();
        }

        public bool TryToUInt64(out ulong @ulong)
        {
            if (IsAbsent())
            {
                @ulong = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long @long)
            {
                @ulong = NumericConverter.TryConvert(
                    @long,
                    out ulong result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );
                return true;
            }
            throw new ApplicationException();
        }

        public bool TryToUInt8(out byte @byte)
        {
            if (IsAbsent())
            {
                @byte = default;
                return false;
            }
            if (type != ConfigType.Integer)
            {
                throw new ArgumentException($"Config item '{path}' type is not 'integer'.");
            }

            if (@object is long @long)
            {
                @byte = NumericConverter.TryConvert(
                    @long,
                    out byte result,
                    ConversionRule.CheckOverflow
                )
                    ? result
                    : throw new ArgumentException(
                        $"Integer overflow occurred for Config item '{path}'."
                    );
                ;

                return true;
            }
            throw new ApplicationException();
        }

        #endregion TryTo
    }
}
