using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Atomtick.Configuration.Interfaces;

namespace Atomtick.Configuration
{
    public partial class PrimitiveConfigSource : IConfigValidator
    {
        public bool ValidateValue(string config, string value, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(config))
            {
                errorMessage = "Config item name cannot be null or empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                errorMessage = ExceptionHelper.GetFormattedString("Value_Empty_Null", config);
                return false;
            }
            if (!_configItems.TryGetValue(config, out ConfigItem configItem))
            {
                errorMessage = $"Config item '{config}' not found.";
                return false;
            }
            ConfigType configType = configItem.Type;
            string trimmedValue = value.Trim();

            #region CS Data Type Validation

            double doubleNumber = 0;
            long longNumber = 0;

            if (configType == ConfigType.String)
            {
                ; // do nothing
            }
            else if (configType == ConfigType.Integer)
            {
                if (!TryParse2Int64(trimmedValue, out longNumber))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Integer", trimmedValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Bool)
            {
                if (!bool.TryParse(trimmedValue, out _))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Boolean", trimmedValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Decimal)
            {
                if (!TryParse2Double(trimmedValue, out doubleNumber))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Double", trimmedValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.File)
            {
                if (TryParse2File(trimmedValue, out var _) == false)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", trimmedValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Folder)
            {
                if (TryParse2Directory(trimmedValue, out var _) == false)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", trimmedValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.DateTime)
            {
                if (!TryParse2DateTime(trimmedValue, out _))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2DateTime", trimmedValue, config);
                    return false;
                }
            }
            else if (configType == ConfigType.Color)
            {
                if (!TryParse2Color(trimmedValue, out _))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Color", trimmedValue, config);
                    return false;
                }
            }

            #endregion CS Data Type Validation

            #region Options Validation

            var options = configItem.Options;
            if (options != null && options.Count > 0)
            {
                if (configType == ConfigType.String || configType == ConfigType.Color)
                {
                    if (!options.Contains(trimmedValue))
                    {
                        errorMessage = $"The value '{trimmedValue}' is not in the options for config item '{config}'.";
                        return false;
                    }
                }
                else if (configType == ConfigType.Integer)
                {
                    var longOptions = new List<long>();
                    foreach (var option in options)
                    {
                        TryParse2Int64(option, out long longValue);

                        longOptions.Add(longValue);
                    }
                    TryParse2Int64(trimmedValue, out long @long); // 肯定返回true，因为前面已经调用此函数校验过字符串了
                    if (!longOptions.Contains(@long))
                    {
                        errorMessage = $"The value '{trimmedValue}' is not in the options for config item '{config}'.";
                        return false;
                    }
                }
                else if (configType == ConfigType.Decimal)
                {
                    var doubleOptions = new List<double>();
                    foreach (var option in options)
                    {
                        TryParse2Double(option, out double doubleValue);

                        doubleOptions.Add(doubleValue);
                    }
                    TryParse2Double(trimmedValue, out double @double); // 肯定返回true，因为前面已经调用此函数校验过字符串了。
                    if (!doubleOptions.Contains(@double))
                    {
                        errorMessage = $"The value '{trimmedValue}' is not in the options for config item '{config}'.";
                        return false;
                    }
                }
            }

            #endregion Options Validation

            #region Max & Min Validation

            if (configType == ConfigType.Integer)
            {
                TryParse2Int64(configItem.MaxValue, out var max);
                TryParse2Int64(configItem.MinValue, out var min);
                if (longNumber > max || longNumber < min)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", trimmedValue, config, configItem.MinValue, configItem.MaxValue);
                    return false;
                }
            }

            if (configType == ConfigType.Decimal)
            {
                TryParse2Double(configItem.MaxValue, out var max);
                TryParse2Double(configItem.MinValue, out var min);
                if (doubleNumber > max || doubleNumber < min)
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", trimmedValue, config, configItem.MinValue, configItem.MaxValue);
                    return false;
                }
            }

            #endregion Max & Min Validation

            #region Regular Expression Validation

            var regex = configItem.Regex;
            var vtype = configItem.Type;
            if (!string.IsNullOrWhiteSpace(regex))
            {
                if ((vtype == ConfigType.String || vtype == ConfigType.File || vtype == ConfigType.Folder || vtype == ConfigType.DateTime || vtype == ConfigType.Color) && !Regex.IsMatch(trimmedValue, regex))
                {
                    errorMessage = ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", trimmedValue, configItem.RegexNote, config);
                    return false;
                }
                else if (vtype == ConfigType.Decimal)
                {
                    if (TryParse2Double(trimmedValue, out doubleNumber))
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
                    if (TryParse2Int64(trimmedValue, out longNumber))
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

            if (Settings.AppendedValidationRule != null && Settings.AppendedValidationRule?.Invoke(config, value, this) == false)
            {
                errorMessage = ExceptionHelper.GetFormattedString("ArgumentException_CustomizeValidation", trimmedValue, config);
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
    }
}
