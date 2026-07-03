using System.Runtime.CompilerServices;

namespace SCADA.Common
{
    /// <summary>
    /// Object => 泛型
    /// </summary>
    public static partial class NumericConverter
    {
        // 运行时传入具体类型后，if (typeof(T) == typeof(xxx))的结果是确定的，不成立的IF全被被移除(去除死代码)
        // 成立的条件会被编译成常量，所以这个函数在实际执行时，不会执行任何判断，速度极快
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(object @object, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (CastToInt8(@object, out sbyte res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(byte))
            {
                if (CastToUInt8(@object, out byte res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(short))
            {
                if (CastToInt16(@object, out short res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (CastToUInt16(@object, out ushort res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(int))
            {
                if (CastToInt32(@object, out int res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(uint))
            {
                if (CastToUInt32(@object, out uint res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(long))
            {
                if (CastToInt64(@object, out long res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (CastToUInt64(@object, out ulong res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (CastToDecimal(@object, out decimal res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (CastToDouble(@object, out double res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(float))
            {
                if (CastToFloat(@object, out float res, rule))
                {
#if NET462_OR_GREATER
                    number = (T)(object)res;
#elif NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);// 0 开销强制转换
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }
            number = default;
            return false;
        }

        #region 内部转换逻辑 (Fast Paths)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToDecimal(object @object, out decimal v, ConversionRule rule)
        {
            if (@object is decimal @decimal) { v = @decimal; return true; }
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToDouble(object @object, out double v, ConversionRule rule)
        {
            if (@object is double @double) { v = @double; return true; }
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToFloat(object @object, out float v, ConversionRule rule)
        {
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToInt16(object @object, out short v, ConversionRule rule)
        {
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToInt32(object @object, out int v, ConversionRule rule)
        {
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToInt64(object @object, out long v, ConversionRule rule)
        {
            if (@object is long @long) { v = @long; return true; }
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToInt8(object @object, out sbyte v, ConversionRule rule)
        {
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToUInt16(object @object, out ushort v, ConversionRule rule)
        {
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToUInt32(object @object, out uint v, ConversionRule rule)
        {
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToUInt64(object @object, out ulong v, ConversionRule rule)
        {
            if (@object is ulong @ulong) { v = @ulong; return true; }
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CastToUInt8(object @object, out byte v, ConversionRule rule)
        {
            if (@object is byte @byte) return TryConvert(@byte, out v, rule);
            if (@object is sbyte @sbyte) return TryConvert(@sbyte, out v, rule);
            if (@object is short @short) return TryConvert(@short, out v, rule);
            if (@object is int @int) return TryConvert(@int, out v, rule);
            if (@object is long @long) return TryConvert(@long, out v, rule);
            if (@object is ushort @ushort) return TryConvert(@ushort, out v, rule);
            if (@object is uint @uint) return TryConvert(@uint, out v, rule);
            if (@object is ulong @ulong) return TryConvert(@ulong, out v, rule);
            if (@object is double @double) return TryConvert(@double, out v, rule);
            if (@object is float @float) return TryConvert(@float, out v, rule);
            if (@object is decimal @decimal) return TryConvert(@decimal, out v, rule);
            v = default; return false;
        }

        #endregion 内部转换逻辑 (Fast Paths)
    }
}