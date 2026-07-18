using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource
    {
        public bool ValidateValue(string config, string value, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(config))
            {
                errorMessage = "Config item cannot be null or empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                errorMessage = "Config value cannot be null or empty.";
                return false;
            }
            if (!_configItems.TryGetValue(config, out ConfigItem configItem))
            {
                errorMessage = $"Config item '{config}' not found.";
                return false;
            }
            ConfigType configType = configItem.Type;
            string strValue = value.Trim();

            #region CS Data Type Validation

            double doubleNumber = 0;
            long longNumber = 0;

            if (configType == ConfigType.String)
            {
                ; // do nothing
            }
            else if (configType == ConfigType.Integer)
            {
                if (!StringParser.TryParse2Int64(strValue, out longNumber))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Integer", strValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Bool)
            {
                if (!bool.TryParse(strValue, out _))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Boolean", strValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Decimal)
            {
                if (!StringParser.TryParse2Double(strValue, out doubleNumber))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Double", strValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.File)
            {
                if (StringParser.TryParse2File(strValue, out var _) == false)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", strValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Folder)
            {
                if (StringParser.TryParse2Directory(strValue, out var _) == false)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", strValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.DateTime)
            {
                if (!StringParser.TryParse2DateTime(strValue, out _))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2DateTime", strValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Color)
            {
                if (!StringParser.TryParse2Color(strValue, out _))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Color", strValue, config);
                    return false;
                }
            }

            #endregion CS Data Type Validation

            #region Options Validation

            var options = configItem.Options;
            var type = configItem.Type;
            if (options != null && options.Length > 0)
            {
                if (type == ConfigType.String)
                {
                    if (!options.Contains(strValue))
                    {
                        errorMessage = $"The value '{strValue}' is not in the options for config item '{config}'.";
                        return false;
                    }
                }
                else if (type == ConfigType.Integer)
                {
                    var longOptions = new List<long>();
                    foreach (var option in options)
                    {
                        if (StringParser.TryParse2Int64(option, out long longValue))
                        {
                            longOptions.Add(longValue);
                        }
                        else
                        {
                            errorMessage = $"option '{option}' can't convert to a integer for '{config}'.";
                            return false;
                        }
                    }
                    StringParser.TryParse2Int64(strValue, out long @long); // 肯定返回true，因为前面已经调用此函数校验过字符串了
                    if (!longOptions.Contains(@long))
                    {
                        errorMessage = $"The value '{strValue}' is not in the options for config item '{config}'.";
                        return false;
                    }
                }
                else if (type == ConfigType.Decimal)
                {
                    var doubleOptions = new List<double>();
                    foreach (var option in options)
                    {
                        if (StringParser.TryParse2Double(option, out double doubleValue))
                        {
                            doubleOptions.Add(doubleValue);
                        }
                        else
                        {
                            errorMessage = $"option '{option}' can't convert to a double for '{config}'.";
                            return false;
                        }
                    }
                    StringParser.TryParse2Double(strValue, out double @double); // 肯定返回true，因为前面已经调用此函数校验过字符串了。
                    if (!doubleOptions.Contains(@double))
                    {
                        errorMessage = $"The value '{strValue}' is not in the options for config item '{config}'.";
                        return false;
                    }
                }
            }

            #endregion Options Validation

            #region Max & Min Validation

            if (configType == ConfigType.Integer)
            {
                StringParser.TryParse2Int64(configItem.MaxValue, out var max);
                StringParser.TryParse2Int64(configItem.MinValue, out var min);
                if (longNumber > max || longNumber < min)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", strValue, config, configItem.MinValue, configItem.MaxValue);
                    return false;
                }
            }

            if (configType == ConfigType.Decimal)
            {
                StringParser.TryParse2Double(configItem.MaxValue, out var max);
                StringParser.TryParse2Double(configItem.MinValue, out var min);
                if (doubleNumber > max || doubleNumber < min)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", strValue, config, configItem.MinValue, configItem.MaxValue);
                    return false;
                }
            }

            #endregion Max & Min Validation

            #region Regular Expression Validation

            var regex = configItem.Regex;
            var vtype = configItem.Type;
            if (!string.IsNullOrWhiteSpace(regex))
            {
                if ((vtype == ConfigType.String || vtype == ConfigType.File || vtype == ConfigType.Folder || vtype == ConfigType.DateTime) && !Regex.IsMatch(strValue, regex))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", strValue, configItem.RegexNote, config);
                    return false;
                }
                else if (vtype == ConfigType.Decimal)
                {
                    if (StringParser.TryParse2Double(strValue, out doubleNumber))
                    {
                        if (!Regex.IsMatch(doubleNumber.ToString(CultureInfo.InvariantCulture), regex))
                        {
                            errorMessage = ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", doubleNumber.ToString(CultureInfo.InvariantCulture), configItem.RegexNote, config);
                            return false;
                        }
                    }
                }
                else if (vtype == ConfigType.Integer)
                {
                    if (StringParser.TryParse2Int64(strValue, out longNumber))
                    {
                        if (!Regex.IsMatch(longNumber.ToString(CultureInfo.InvariantCulture), regex))
                        {
                            errorMessage = ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", longNumber.ToString(CultureInfo.InvariantCulture), configItem.RegexNote, config);
                            return false;
                        }
                    }
                }
            }

            #endregion Regular Expression Validation

            #region Appended Validation Rule
            if (Settings.AppendedValidationRule != null)
                if (Settings.AppendedValidationRule?.Invoke(config, value, this) == false)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentException_CustomizeValidation", strValue, config);
                    return false;
                }
            #endregion Appended Validation Rule
            errorMessage = null;
            return true;
        }

        public void ValidateValue(string config, string value)
        {
            if (!ValidateValue(config, value, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        //public void ValidateValue(string config, string value)
        //{
        //    if (string.IsNullOrWhiteSpace(config))
        //    {
        //        throw new ArgumentException("Config item cannot be null or empty.", nameof(config));
        //    }
        //    if (string.IsNullOrWhiteSpace(value))
        //    {
        //        throw new ArgumentException("Config value cannot be null or empty.", nameof(value));
        //    }
        //    if (!_configItems.TryGetValue(config, out ConfigItem configItem))
        //    {
        //        throw new KeyNotFoundException($"Config item '{config}' not found.");
        //    }
        //    ConfigType configType = configItem.Type;
        //    string strValue = value.Trim();

        //    #region CS Data Type Validation

        //    double doubleNumber = 0;
        //    long longNumber = 0;

        //    if (configType == ConfigType.String)
        //    {
        //        ; // do nothing
        //    }
        //    else if (configType == ConfigType.Integer)
        //    {
        //        if (!StringParser.TryParse2Int64(strValue, out longNumber))
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Integer", strValue, config));
        //        }
        //    }
        //    else if (configType == ConfigType.Bool)
        //    {
        //        if (!bool.TryParse(strValue, out _))
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Boolean", strValue, config));
        //        }
        //    }
        //    else if (configType == ConfigType.Decimal)
        //    {
        //        if (!StringParser.TryParse2Double(strValue, out doubleNumber))
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Double", strValue, config));
        //        }
        //    }
        //    else if (configType == ConfigType.File)
        //    {
        //        if (StringParser.TryParse2File(strValue, out var _) == false)
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", strValue, config));
        //        }
        //    }
        //    else if (configType == ConfigType.Folder)
        //    {
        //        if (StringParser.TryParse2Directory(strValue, out var _) == false)
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", strValue, config));
        //        }
        //    }
        //    else if (configType == ConfigType.DateTime)
        //    {
        //        if (!StringParser.TryParse2DateTime(strValue, out _))
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2DateTime", strValue, config));
        //        }
        //    }
        //    else if (configType == ConfigType.Color)
        //    {
        //        if (!StringParser.TryParse2Color(strValue, out _))
        //        {
        //            throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Color", strValue, config));
        //        }
        //    }

        //    #endregion CS Data Type Validation

        //    #region Options Validation

        //    var options = configItem.Options;
        //    var type = configItem.Type;
        //    if (options != null && options.Length > 0)
        //    {
        //        if (type == ConfigType.String)
        //        {
        //            if (!options.Contains(strValue))
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{config}'.");
        //            }
        //        }
        //        else if (type == ConfigType.Integer)
        //        {
        //            var longOptions = new List<long>();
        //            foreach (var option in options)
        //            {
        //                if (StringParser.TryParse2Int64(option, out long longValue))
        //                {
        //                    longOptions.Add(longValue);
        //                }
        //                else
        //                {
        //                    throw new ArgumentException($"option '{option}' can't convert to a integer for '{config}'.");
        //                }
        //            }
        //            StringParser.TryParse2Int64(strValue, out long @long); // 肯定返回true，因为前面已经调用此函数校验过字符串了
        //            if (!longOptions.Contains(@long))
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{config}'.");
        //            }
        //        }
        //        else if (type == ConfigType.Decimal)
        //        {
        //            var doubleOptions = new List<double>();
        //            foreach (var option in options)
        //            {
        //                if (StringParser.TryParse2Double(option, out double doubleValue))
        //                {
        //                    doubleOptions.Add(doubleValue);
        //                }
        //                else
        //                {
        //                    throw new ArgumentException($"option '{option}' can't convert to a double for '{config}'.");
        //                }
        //            }
        //            StringParser.TryParse2Double(strValue, out double @double); // 肯定返回true，因为前面已经调用此函数校验过字符串了。
        //            if (!doubleOptions.Contains(@double))
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{config}'.");
        //            }
        //        }
        //    }

        //    #endregion Options Validation

        //    #region Max & Min Validation

        //    if (configType == ConfigType.Integer)
        //    {
        //        StringParser.TryParse2Int64(configItem.MaxValue, out var max);
        //        StringParser.TryParse2Int64(configItem.MinValue, out var min);
        //        if (longNumber > max || longNumber < min)
        //        {
        //            throw new ArgumentOutOfRangeException("", ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", strValue, config, configItem.MinValue, configItem.MaxValue));
        //        }
        //    }

        //    if (configType == ConfigType.Decimal)
        //    {
        //        StringParser.TryParse2Double(configItem.MaxValue, out var max);
        //        StringParser.TryParse2Double(configItem.MinValue, out var min);
        //        if (doubleNumber > max || doubleNumber < min)
        //        {
        //            throw new ArgumentOutOfRangeException("", ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", strValue, config, configItem.MinValue, configItem.MaxValue));
        //        }
        //    }

        //    #endregion Max & Min Validation

        //    #region Regular Expression Validation

        //    var regex = configItem.Regex;
        //    var vtype = configItem.Type;
        //    if (!string.IsNullOrWhiteSpace(regex))
        //    {
        //        if ((vtype == ConfigType.String || vtype == ConfigType.File || vtype == ConfigType.Folder || vtype == ConfigType.DateTime) && !Regex.IsMatch(strValue, regex))
        //        {
        //            throw new ArgumentException(ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", strValue, configItem.RegexNote, config));
        //        }
        //        else if (vtype == ConfigType.Decimal)
        //        {
        //            if (StringParser.TryParse2Double(strValue, out doubleNumber))
        //            {
        //                if (!Regex.IsMatch(doubleNumber.ToString(CultureInfo.InvariantCulture), regex))
        //                {
        //                    throw new ArgumentException(
        //                        ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", doubleNumber.ToString(CultureInfo.InvariantCulture), configItem.RegexNote, config)
        //                    );
        //                }
        //            }
        //        }
        //        else if (vtype == ConfigType.Integer)
        //        {
        //            if (StringParser.TryParse2Int64(strValue, out longNumber))
        //            {
        //                if (!Regex.IsMatch(longNumber.ToString(CultureInfo.InvariantCulture), regex))
        //                {
        //                    throw new ArgumentException(
        //                        ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", longNumber.ToString(CultureInfo.InvariantCulture), configItem.RegexNote, config)
        //                    );
        //                }
        //            }
        //        }
        //    }

        //    #endregion Regular Expression Validation

        //    #region Appended Validation Rule
        //    if (Settings.AppendedValidationRule != null)
        //        if (Settings.AppendedValidationRule?.Invoke(config, value, this) == false)
        //        {
        //            throw new ArgumentException(ExceptionHelper.GetFormattedString("ArgumentException_CustomizeValidation", strValue, config));
        //        }
        //    #endregion Appended Validation Rule
        //}

        public bool ValidateValue(string config, object value, out string errorMessage)
        {
            if (value == null)
            {
                errorMessage = "Value cannot be null.";
                return false;
            }
            if (
                value is bool
                || value is string
                || value is long
                || value is ulong
                || value is int
                || value is uint
                || value is ushort
                || value is short
                || value is byte
                || value is sbyte
                || value is float
                || value is double
                || value is decimal
                || value is char
                || value is DateTime
                || value is FileInfo
                || value is DirectoryInfo
                || value is System.Drawing.Color
            )
            {
                // 这些类型是支持的,继续往下走正常流程.
            }
            else
            {
                errorMessage = $"Unsupported config value type: {value.GetType().FullName}.";
                return false;
            }
            return ValidateValue(config, Convert2String(value).Trim(), out errorMessage);
        }

        public void ValidateValue(string config, object value)
        {
            if (!ValidateValue(config, value, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
