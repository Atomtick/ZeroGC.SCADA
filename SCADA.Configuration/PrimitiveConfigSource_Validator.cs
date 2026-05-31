using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource
    {
        private void ValidateValue(string config, string value)
        {
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentException("Config item cannot be null or empty.", nameof(config));
            }
            if (!_configItems.TryGetValue(config, out ConfigItem configItem))
            {
                throw new KeyNotFoundException($"Config item '{config}' not found.");
            }
            ConfigType configType = configItem.Type;
            string strValue = value.Trim();

            #region CS Data Type Validation

            double number = 0;

            if (configType == ConfigType.String)
            {
                ; // do nothing
            }
            else if (configType == ConfigType.Integer)
            {
                if (StringParser.TryParse2Long(strValue, out long @long))
                {
                    number = @long;
                }
                else
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Integer", strValue, config));
                }
            }
            else if (configType == ConfigType.Bool)
            {
                if (!bool.TryParse(strValue, out _))
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Boolean", strValue, config));
                }
            }
            else if (configType == ConfigType.Decimal)
            {
                if (!StringParser.TryParse2Double(strValue, out number))
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Double", strValue, config));
                }
            }
            else if (configType == ConfigType.File)
            {
                if (StringParser.TryParse2File(strValue, out var _) == false)
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", strValue, config));
                }
            }
            else if (configType == ConfigType.Folder)
            {
                if (StringParser.TryParse2Directory(strValue, out var _) == false)
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Path", strValue, config));
                }
            }
            else if (configType == ConfigType.DateTime)
            {
                if (!StringParser.TryParse2DateTime(strValue, out _))
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2DateTime", strValue, config));
                }
            }
            else if (configType == ConfigType.Color)
            {
                if (!StringParser.TryParse2Color(strValue, out _))
                {
                    throw new InvalidCastException(ExceptionHelper.GetFormattedString("InvalidCastException_CannotConvert2Color", strValue, config));
                }
            }
            else
            {
                throw new NotSupportedException($"Unsupported value type: {configType}");
            }

            #endregion CS Data Type Validation

            #region Regular Expression Validation

            var regex = configItem.Regex;
            var vtype = configItem.Type;
            if (!string.IsNullOrWhiteSpace(regex))
            {
                if ((vtype == ConfigType.String ||
                    vtype == ConfigType.File ||
                    vtype == ConfigType.Folder)
                    && !Regex.IsMatch(strValue, regex))
                {
                    throw new ArgumentException(ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", strValue, configItem.RegexNote, config));
                }
                else if (vtype == ConfigType.Integer || vtype == ConfigType.Decimal)
                {
                    if (StringParser.TryParse2Double(strValue, out number))
                    {
                        if (!Regex.IsMatch(number.ToString(CultureInfo.InvariantCulture), regex))
                        {
                            throw new ArgumentException(ExceptionHelper.GetFormattedString("ArgumentException_RegexValidation", number.ToString(CultureInfo.InvariantCulture), configItem.RegexNote, config));
                        }
                    }
                }
            }

            #endregion Regular Expression Validation

            #region Options Validation

            var options = configItem.Options;
            var type = configItem.Type;
            if (options != null && options.Length > 0)
            {
                if (type == ConfigType.String)
                {
                    if (!options.Contains(strValue))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{config}'.");
                    }
                }
                else if (type == ConfigType.Integer)
                {
                    var longOptions = new List<long>();
                    foreach (var option in options)
                    {
                        if (StringParser.TryParse2Long(option, out long longValue))
                        {
                            longOptions.Add(longValue);
                        }
                        else
                        {
                            throw new ArgumentException($"option '{option}' can't convert to a integer for '{config}'.");
                        }
                    }
                    StringParser.TryParse2Long(strValue, out long @long); // 肯定返回true，因为前面已经调用此函数校验过字符串了。
                    if (!longOptions.Contains(@long))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{config}'.");
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
                            throw new ArgumentException($"option '{option}' can't convert to a double for '{config}'.");
                        }
                    }
                    StringParser.TryParse2Double(strValue, out double @double); // 肯定返回true，因为前面已经调用此函数校验过字符串了。
                    if (!doubleOptions.Contains(@double))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{config}'.");
                    }
                }
            }

            #endregion Options Validation

            #region Max & Min Validation

            if (configType == ConfigType.Integer || configType == ConfigType.Decimal)
            {
                StringParser.TryParse2Double(configItem.MaxValue, out var max);
                StringParser.TryParse2Double(configItem.MinValue, out var min);
                if (number > max || number < min)
                {
                    throw new ArgumentOutOfRangeException("", ExceptionHelper.GetFormattedString("ArgumentOutOfRangeException_MaxMin", strValue, config, configItem.MinValue, configItem.MaxValue));
                }
            }

            #endregion Max & Min Validation

            #region Customized Rule Validation

            var validationRule = CustomizeValidationRule(this,config);
            if (validationRule != null && !validationRule.Invoke(config))
            {
                throw new ArgumentException(ExceptionHelper.GetFormattedString("ArgumentException_CustomizeValidation", strValue, config));
            }

            #endregion Customized Rule Validation
        }

        private void ValidateValue(string config, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            ValidateValue(config, Convert2String(value).Trim());
        }
    }
}