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

        // 提前缓存，避免堆分配
        private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

        // 静态只读字段缓存 OS 判断，运行时开销为 0
        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        ///
        /// </summary>
        /// <param name="string">必须是#XXXXXX或#XXXXXXXX;X是16进制字符,不区分大小写</param>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static bool TryParse2Color(string @string, out System.Drawing.Color color)
        {
            color = System.Drawing.Color.Empty;
            if (string.IsNullOrWhiteSpace(@string))
            {
                return false;
            }
            if (!@string.StartsWith("#"))
            {
                return false;
            }
            if (@string.Length != 7 && @string.Length != 9)
            {
                return false;
            }
            if (!Regex.IsMatch(@string, @"^#[0-9a-fA-F]+$"))
            {
                return false;
            }
            color = System.Drawing.ColorTranslator.FromHtml(@string);
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="string">字符串格式必须严格是yyyyMMddHHmmss;共计14个字符,多一个少一个都不行</param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        internal static bool TryParse2DateTime(string @string, out DateTime dateTime)
        {
            if (string.IsNullOrWhiteSpace(@string))
            {
                dateTime = default;
                return false;
            }
            if (DateTime.TryParseExact(@string, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return true;
            }
            else if (DateTime.TryParseExact(@string, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return true;
            }
            else if (DateTime.TryParseExact(@string, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return true;
            }
            dateTime = default;
            return false;
        }

        internal static bool TryParse2Directory(string @string, out DirectoryInfo directoryInfo)
        {
            directoryInfo = null;
            // 1. 基础非空校验
            if (string.IsNullOrWhiteSpace(@string))
                return false;

            // 2. 检查系统内置的基础非法路径字符（主要包含 ASCII 0-31 控制字符）
            if (@string.IndexOfAny(InvalidPathChars) >= 0)
                return false;

            // 3. 跨平台明确性约束：无论什么 OS，既然要求是“明确的路径”，就必须无条件拒绝通配符
            if (@string.Contains('*') || @string.Contains('?'))
                return false;

            // 4. 操作系统差异化前置字符拦截
            if (IsWindows)
            {
                // Windows 专属高危字符拦截
                if (@string.Contains('<') || @string.Contains('>') || @string.Contains('"'))
                    return false;
            }
            // 注：Linux/macOS 物理底层对 < > " 是包容的，所以此处无需拦截。

            try
            {
                // 5. 利用 BCL 核心 API 进行系统级路径正常化
                // 这一步会自动将相对路径转为绝对路径，并验证长路径、非法拓扑等系统级规则

                string fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @string));

                // ==========================================
                // 注意：这里【没有】包含检查结尾分隔符的逻辑，
                // 也【没有】包含 GetFileName() 的非空检查，
                // 因为 "C:\" 或 "/etc/" 作为文件夹路径是绝对合法的。
                // ==========================================

                // 6. 操作系统差异化深度校验：冒号与盘符逻辑
                if (IsWindows)
                {
                    // Windows：严格校验盘符冒号
                    int firstColon = fullPath.IndexOf(':');
                    if (firstColon >= 0)
                    {
                        // 冒号必须在索引 1（如 C:），且绝不能有第二个冒号（防止 ADS 数据流注入或畸形路径）
                        if (firstColon != 1 || fullPath.IndexOf(':', 2) >= 0)
                        {
                            return false;
                        }
                    }
                }
                // 注：Linux/macOS 全局无盘符概念，全路径如 /opt/data:backup/ 是完全合法的文件夹名
                directoryInfo = new DirectoryInfo(fullPath);
                return true;
            }
            catch (Exception)
            {
                // 顺畅捕获底层 IO 抛出的 ArgumentException, PathTooLongException 等格式异常
                return false;
            }
        }

        internal static bool TryParse2Double(string @string, out double @double)
        {
            // 允许整数形式的字符串转换成double
            if (long.TryParse(@string, NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out long @long))
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
            else if (@string.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && long.TryParse(@string.TrimStart('0', 'x', 'X'), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out @long))
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
            else if (double.TryParse(@string, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.Float, CultureInfo.InvariantCulture, out double decValue))
            {
                @double = decValue;
                return true;
            }
            else
            {
                @double = default;
                return false;
            }
        }

        internal static bool TryParse2File(string @string, out FileInfo fileInfo)
        {
            fileInfo = null;
            if (string.IsNullOrWhiteSpace(@string))
            {
                return false;
            }

            // 1. 系统内置基础过滤（Linux 下通常只有 \0，Windows 下有控制字符和 |）
            if (@string.IndexOfAny(InvalidPathChars) >= 0)
                return false;

            // 2. 跨平台明确性约束：无论什么 OS，既然要求是“明确路径”，就必须拒绝通配符
            // 尽管 Linux 物理上允许文件名带 * 和 ?，但在业务逻辑中它们通常会引发 Shell 解析灾难，必须拦截
            if (@string.Contains('*') || @string.Contains('?'))
                return false;

            // 3. 操作系统差异化前置字符拦截
            if (IsWindows)
            {
                // Windows 专属高危字符拦截
                if (@string.Contains('<') || @string.Contains('>') || @string.Contains('"'))
                    return false;
            }
            else
            {
                // Linux/macOS 物理上允许 < > "，此处放行。
                // 但如果您的业务依然想在所有平台统一屏蔽这些特殊符号，可以取消 if (IsWindows) 的判断
            }

            try
            {
                // 4. BCL 核心 API 系统级路径正常化
                string fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @string));

                // 5. 确保不是以目录分隔符结尾（明确的“文件”路径尾部不该有分隔符）
                if (@string.EndsWith(Path.DirectorySeparatorChar.ToString()) || @string.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                {
                    return false;
                }

                // 6. 提取文件名合规校验
                var fileName = Path.GetFileName(fullPath);
                if (string.IsNullOrWhiteSpace(fileName))
                    return false;

                // 7. 操作系统差异化深度校验：冒号与盘符逻辑
                if (IsWindows)
                {
                    // Windows：严格校验盘符冒号
                    int firstColon = fullPath.IndexOf(':');
                    if (firstColon >= 0)
                    {
                        // 冒号必须在索引1（如C:），且绝不能有第二个冒号（防止 ADS 数据流注入）
                        if (firstColon != 1 || fullPath.IndexOf(':', 2) >= 0)
                        {
                            return false;
                        }
                    }
                }
                // 注意：Linux/macOS 直接跳过冒号校验，因为全路径 /home/usr/test:1.txt 是完全合法的
                fileInfo = new FileInfo(fullPath);
                return true;
            }
            catch (Exception)
            {
                // 捕获跨平台底层 IO 的格式异常
                return false;
            }
        }

        internal static bool TryParse2Int64(string @string, out long @long)
        {
            if (long.TryParse(@string, NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out @long))
            {
                return true;
            }
            else if (@string.StartsWith("0X", StringComparison.OrdinalIgnoreCase) && long.TryParse(@string.TrimStart('0', 'x', 'X'), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out @long))
            {
                return true;
            }
            return false;
        }
    }
}
