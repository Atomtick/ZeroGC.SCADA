using System;
using System.Runtime.CompilerServices;

namespace SCADA.Common
{
    /* 这是一份支持数值类型之间相互转换的代码文件,支持溢出检查和精度损失检查.
     * 1. 全程无try catch. 2. 不会抛出异常
     * 3. 全程无装箱 4. 在不可能溢出的情况下不要检查溢出.
     * 5. 在不可能损失精度的情况下不要类型回转浪费时间.
     */

    /// <summary>
    /// 具体类型 <=> 具体类型
    /// </summary>
    public static partial class NumericConverter
    {
        private const double SafeDoubleMaxForDecimal = 7.922816251426433E+28d;

        // 预定义 Decimal 安全转换边界常量
        private const double SafeDoubleMinForDecimal = -7.922816251426433E+28d;

        private const float SafeFloatMaxForDecimal = 7.922816E+28f;
        private const float SafeFloatMinForDecimal = -7.922816E+28f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out short result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < ushort.MinValue) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out int result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < uint.MinValue) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out long result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < 0) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(sbyte input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out sbyte result, ConversionRule rule)
        {
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out short result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out ushort result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out int result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out uint result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out long result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out ulong result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(byte input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < sbyte.MinValue || input > sbyte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue || input > byte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < ushort.MinValue) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out int result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < 0) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out long result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < 0) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(short input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input > sbyte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue || input > byte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out short result, ConversionRule rule)
        {
            unchecked { result = (short)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out int result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out uint result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out long result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out ulong result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ushort input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < sbyte.MinValue || input > sbyte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue || input > byte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < short.MinValue || input > short.MaxValue) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < ushort.MinValue || input > ushort.MaxValue) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < uint.MinValue) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out long result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < 0) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((int)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(int input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input > sbyte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue || input > byte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input > short.MaxValue) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < ushort.MinValue || input > ushort.MaxValue) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out int result, ConversionRule rule)
        {
            unchecked { result = (int)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out long result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out ulong result, ConversionRule rule)
        {
            unchecked { result = input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((uint)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(uint input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < sbyte.MinValue || input > sbyte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue || input > byte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < short.MinValue || input > short.MaxValue) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < ushort.MinValue || input > ushort.MaxValue) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out int result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < int.MinValue || input > int.MaxValue) { result = default; return false; }
            }
            unchecked { result = (int)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < uint.MinValue || input > uint.MaxValue) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < 0) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((long)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((long)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(long input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input > (ulong)sbyte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < byte.MinValue || input > byte.MaxValue) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input > (int)short.MaxValue) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < ushort.MinValue || input > ushort.MaxValue) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out int result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input > int.MaxValue) { result = default; return false; }
            }
            unchecked { result = (int)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < uint.MinValue || input > uint.MaxValue) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out long result, ConversionRule rule)
        {
            unchecked { result = (long)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((ulong)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out double result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((ulong)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(ulong input, out decimal result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowIntToFloat) == 0) { result = default; return false; }
            unchecked { result = input; }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= sbyte.MinValue && input <= sbyte.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= byte.MinValue && input <= byte.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= short.MinValue && input <= short.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= ushort.MinValue && input <= ushort.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out int result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= int.MinValue && input <= int.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (int)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= uint.MinValue && input <= uint.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out long result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if (float.IsNaN(input)) { result = default; return false; } // ✅ 修复 NaN
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= long.MinValue && input < ((float)9223372036854775808.0f))) { result = default; return false; }
            }
            unchecked { result = (long)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if (float.IsNaN(input)) { result = default; return false; } // ✅
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= ((float)0.0) && input < ((float)18446744073709551616.0f))) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out double result, ConversionRule rule)
        {
            unchecked { result = (double)input; }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(float input, out decimal result, ConversionRule rule)
        {
            if (!(input >= SafeFloatMinForDecimal && input <= SafeFloatMaxForDecimal)) { result = default; return false; }
            unchecked { result = (decimal)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((float)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= sbyte.MinValue && input <= sbyte.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= byte.MinValue && input <= byte.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= short.MinValue && input <= short.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= ushort.MinValue && input <= ushort.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out int result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= int.MinValue && input <= int.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (int)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= uint.MinValue && input <= uint.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out long result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if (double.IsNaN(input)) { result = default; return false; } // ✅
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= long.MinValue && input < ((double)9223372036854775808.0d))) { result = default; return false; }
            }
            unchecked { result = (long)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if (double.IsNaN(input)) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= ((double)0.0) && input < ((double)18446744073709551616.0d))) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out float result, ConversionRule rule)
        {
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (input < float.MinValue || input > float.MaxValue) { result = default; return false; }
            }
            unchecked { result = (float)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((double)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(double input, out decimal result, ConversionRule rule)
        {
            if (!(input >= SafeDoubleMinForDecimal && input <= SafeDoubleMaxForDecimal)) { result = default; return false; }
            unchecked { result = (decimal)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((double)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out sbyte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= sbyte.MinValue && input <= sbyte.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (sbyte)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out byte result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= byte.MinValue && input <= byte.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (byte)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out short result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= short.MinValue && input <= short.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (short)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out ushort result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= ushort.MinValue && input <= ushort.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (ushort)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out int result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= int.MinValue && input <= int.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (int)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out uint result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= uint.MinValue && input <= uint.MaxValue)) { result = default; return false; }
            }
            unchecked { result = (uint)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out long result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= long.MinValue && input < ((decimal)9223372036854775808.0d))) { result = default; return false; }
            }
            unchecked { result = (long)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out ulong result, ConversionRule rule)
        {
            if ((rule & ConversionRule.AllowFloatToInt) == 0) { result = default; return false; }
            if ((rule & ConversionRule.CheckOverflow) != 0)
            {
                if (!(input >= ((decimal)0.0) && input < ((decimal)18446744073709551616.0d))) { result = default; return false; }
            }
            unchecked { result = (ulong)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if (result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out float result, ConversionRule rule)
        {
            unchecked { result = (float)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((decimal)result != input) return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert(decimal input, out double result, ConversionRule rule)
        {
            unchecked { result = (double)input; }
            if ((rule & ConversionRule.CheckPrecision) != 0)
            {
                if ((decimal)result != input) return false;
            }
            return true;
        }
    }
}