using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SCADA.Configuration
{
    // InvariantCulture（固定区域性）是一种独立于任何国家、地区或语言的格式。它的底层逻辑基于英语文化，但它被硬编码在.NET 中，永远不会随着操作系统的设置而改变。
    // 使用 CultureInfo.InvariantCulture 的核心目的，正是为了保证你的程序和数据在拷贝到不同区域的电脑上时，绝对不会因为区域设置不同而崩溃或无法使用
    internal static class StringParser
    {
        public static readonly long _longMax = long.Parse("9007199254740992");
        public static readonly long _longMin = long.Parse("-9007199254740992");

        /// <summary>
        ///
        /// </summary>
        /// <param name="ColorString">必须是#XXXXXX或#XXXXXXXX;X是16进制字符,不区分大小写</param>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static bool TryParse2Color(string ColorString, out System.Drawing.Color color)
        {
            color = System.Drawing.Color.Empty;
            if (string.IsNullOrWhiteSpace(ColorString))
            {
                return false;
            }
            if (!ColorString.StartsWith("#"))
            {
                return false;
            }
            if (ColorString.Length != 7 && ColorString.Length != 9)
            {
                return false;
            }
            if (!Regex.IsMatch(ColorString, @"^#[0-9a-fA-F]+$"))
            {
                return false;
            }
            color = System.Drawing.ColorTranslator.FromHtml(ColorString);
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="datetimeString">字符串格式必须严格是yyyyMMddHHmmss;共计14个字符,多一个少一个都不行</param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        internal static bool TryParse2DateTime(string datetimeString, out DateTime dateTime)
        {
            if (string.IsNullOrWhiteSpace(datetimeString))
            {
                dateTime = default;
                return false;
            }
            return DateTime.TryParseExact(datetimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
        }

        internal static bool TryParse2Directory(string directoryFullPathString, out DirectoryInfo directoryInfo)
        {
            bool IsValidWindowsFolderPath(string directoryFullPath, out DirectoryInfo directory)
            {
                directory = null;
                string[] validDrives = { "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                if (string.IsNullOrWhiteSpace(directoryFullPath))
                {
                    return false;
                }
                // 必须是绝对路径
                if (!Path.IsPathRooted(directoryFullPath))
                {
                    return false;
                }
                // 如果是绝对路径,至少有3个字符,且前三个字符是确定的
                if (directoryFullPath.Length < 3 || !directoryFullPath[1].Equals(':') || directoryFullPath[2] != '\\')
                {
                    return false;
                }
                // 必须是正确的盘符
                string drive = directoryFullPath[0].ToString().ToUpper();
                if (!validDrives.Any(x => x == drive))
                {
                    return false;
                }
                // 如果刚好是3个字符,则一定是正确的路径,如D:\
                if (directoryFullPath.Length == 3)
                {
                    directory = new DirectoryInfo(directoryFullPath);
                    return true;
                }
                // 去掉盘符以便后续分割
                directoryFullPath = new string(directoryFullPath.Skip(3).ToArray());
                // 不能有连续的 \ (主要是检测末尾是不是有连续的\)
                if (directoryFullPath.Contains("\\\\"))
                {
                    return false;
                }
                // 去除末尾的\后下面就可以分割子串了
                directoryFullPath = directoryFullPath.TrimEnd('\\');
                var subPaths = directoryFullPath.Split('\\');

                foreach (var subPath in subPaths)
                {
                    if (string.IsNullOrWhiteSpace(subPath))
                    {
                        return false;
                    }
                    if (Path.GetInvalidFileNameChars().Any(c => subPath.Contains(c)))
                    {
                        return false;
                    }
                }
                directory = new DirectoryInfo(directoryFullPath);
                return true;
            }
            directoryInfo = null;
#if NET462_OR_GREATER
            return IsValidWindowsFolderPath(directoryFullPathString, out directoryInfo);
#elif NET8_0_OR_GREATER
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return IsValidWindowsFolderPath(directoryFullPathString, out directoryInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // Linux / macOS 规则 (POSIX)
                if (!Path.IsPathRooted(directoryFullPathString))
                {
                    return false;
                }
                // 1. 必须是绝对路径（以 / 开头）
                if (directoryFullPathString[0] != '/') return false;

                // 2. 单遍扫描
                for (int i = 0; i < directoryFullPathString.Length; i++)
                {
                    char c = directoryFullPathString[i];

                    // 规则 A: POSIX 唯一硬性规定的非法字符是 \0
                    if (c == '\0') return false;

                    // 规则 B: 拒绝连续斜杠 (拦截 //usr//bin)
                    if (c == '/' && i > 0 && directoryFullPathString[i - 1] == '/')
                        return false;
                }
                directoryInfo = new DirectoryInfo(directoryFullPathString);
                return true;
            }
            throw new InvalidOperationException("Not supported OS.");
#endif
        }

        internal static bool TryParse2Double(string doubleString, out double @double)
        {
            // 允许整数形式的字符串转换成double
            if (long.TryParse(doubleString, NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out long @long))
            {
                // long太大转换成double时会有精度损失
                if (@long < _longMin || @long > _longMax)
                {
                    @double = default;
                    return false;
                }
                @double = @long;
                return true;
            }
            else if (doubleString.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && long.TryParse(doubleString.TrimStart('0', 'x', 'X'), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out @long))
            {
                // long太大转换成double时会有精度损失
                if (@long < _longMin || @long > _longMax)
                {
                    @double = default;
                    return false;
                }
                @double = @long;
                return true;
            }
            else if (decimal.TryParse(doubleString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.Float, CultureInfo.InvariantCulture, out decimal decValue))
            {
                double tempDouble = (double)decValue;
                decimal castBackDec = (decimal)tempDouble; // C# 底层会在这里强制截取 15 位有效数字

                if (decValue != castBackDec)
                {
                    @double = default;
                    return false; // 发生精度丢失！
                }
                @double = tempDouble;
                return true;
            }
            else
            {
                @double = default;
                return false;
            }
        }

        internal static bool TryParse2File(string fileFullPathString, out FileInfo fileInfo)
        {
            bool IsValidWindowsFilePath(string fileFullPath, out FileInfo file)
            {
                file = null;
                if (string.IsNullOrWhiteSpace(fileFullPath))
                {
                    return false;
                }

                string[] validDrives = { "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                // 必须是绝对路径
                if (!Path.IsPathRooted(fileFullPath))
                {
                    return false;
                }
                // 开头必须是[C-Z]:\
                if (fileFullPath.Length < 3 || !fileFullPath[1].Equals(':') || fileFullPath[2] != '\\')
                {
                    return false;
                }
                string drive = fileFullPath[0].ToString().ToUpper();
                if (!validDrives.Any(x => x == drive))
                {
                    return false;
                }
                // 既然是文件名称,那路径字符串最后一个字符一定不能是'\'
                if (fileFullPath.EndsWith("\\"))
                {
                    return false;
                }
                // 去掉盘符以便后续分割出子片段
                fileFullPath = new string(fileFullPath.Skip(3).ToArray());
                // 只有3个字符的话则一定不是文件名称
                if (string.IsNullOrWhiteSpace(fileFullPath))
                {
                    return false;
                }
                // 子片段路径不能含有Windows不支持的字符以及空白字符
                var subPaths = fileFullPath.Split('\\');
                foreach (var subPath in subPaths)
                {
                    if (string.IsNullOrWhiteSpace(subPath))
                    {
                        return false;
                    }
                    if (Path.GetInvalidFileNameChars().Any(c => subPath.Contains(c)))
                    {
                        return false;
                    }
                }
                file = new FileInfo(fileFullPath);
                return true;
            }

#if NET462_OR_GREATER
            return IsValidWindowsFilePath(fileFullPathString, out fileInfo);
#elif NET8_0_OR_GREATER
            fileInfo = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return IsValidWindowsFilePath(fileFullPathString, out fileInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // Linux / macOS 规则 (POSIX)
                if (!Path.IsPathRooted(fileFullPathString))
                {
                    return false;
                }
                // 1. 必须是绝对路径（以 / 开头）
                if (fileFullPathString[0] != '/') return false;

                // 2. 单遍扫描
                for (int i = 0; i < fileFullPathString.Length; i++)
                {
                    char c = fileFullPathString[i];

                    // 规则 A: POSIX 唯一硬性规定的非法字符是 \0
                    if (c == '\0') return false;

                    // 规则 B: 拒绝连续斜杠 (拦截 //usr//bin)
                    if (c == '/' && i > 0 && fileFullPathString[i - 1] == '/')
                        return false;
                }

                // 结尾不能带斜杠,因为是文件!
                if (fileFullPathString.Length > 1 && fileFullPathString[fileFullPathString.Length - 1] == '/')
                    return false;
                fileInfo = new FileInfo(fileFullPathString);
                return true;
            }
            throw new InvalidOperationException("Not supported OS.");
#endif
        }

        internal static bool TryParse2Long(string longString, out long @long)
        {
            if (long.TryParse(longString, NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out @long))
            {
                return true;
            }
            else if (longString.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && long.TryParse(longString.TrimStart('0', 'x', 'X'), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out @long))
            {
                return true;
            }
            return false;
        }
    }
}