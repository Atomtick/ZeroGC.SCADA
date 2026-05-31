using System;
using System.Runtime.CompilerServices;

namespace SCADA.Common
{
    /// <summary>
    /// 具体类型 => 泛型
    /// </summary>
    public static partial class NumericConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(sbyte @sbyte, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@sbyte, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }

            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@sbyte, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
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
                if (TryConvert(@sbyte, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);// 0 开销强制转换
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else
                {
                    number = default;
                    return false;
                }
            }
            throw new InvalidOperationException($"Unsupported conversion from sbyte to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(byte @byte, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@byte, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@byte, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@byte, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@byte, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@byte, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@byte, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@byte, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@byte, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@byte, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@byte, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@byte, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from byte to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(short @short, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@short, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@short, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@short, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@short, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@short, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@short, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@short, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@short, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@short, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@short, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@short, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from short to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(ushort @ushort, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@ushort, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@ushort, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@ushort, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@ushort, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@ushort, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@ushort, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@ushort, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@ushort, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@ushort, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@ushort, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@ushort, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from ushort to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(int @int, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@int, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@int, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@int, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@int, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@int, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@int, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@int, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@int, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@int, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@int, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@int, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from int to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(uint @uint, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@uint, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@uint, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@uint, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@uint, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@uint, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@uint, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@uint, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@uint, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@uint, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@uint, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@uint, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from uint to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(long @long, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@long, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@long, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@long, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@long, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@long, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@long, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
#if NET10_0_OR_GREATER
                number = Unsafe.As<long, T>(ref @long);
#elif NET462_OR_GREATER
                number = (T)(object)@long;
#endif
                return true;
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@long, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@long, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@long, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@long, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from long to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(ulong @ulong, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@ulong, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@ulong, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@ulong, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@ulong, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@ulong, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@ulong, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@ulong, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
#if NET10_0_OR_GREATER
                number = Unsafe.As<ulong, T>(ref @ulong);
#elif NET462_OR_GREATER
                number = (T)(object)@ulong;
#endif
                return true;
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@ulong, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@ulong, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@ulong, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from ulong to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(decimal @decimal, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@decimal, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@decimal, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@decimal, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@decimal, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@decimal, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@decimal, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@decimal, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@decimal, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
#if NET10_0_OR_GREATER
                number = Unsafe.As<decimal, T>(ref @decimal);
#elif NET462_OR_GREATER
                number = (T)(object)@decimal;
#endif
                return true;
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@decimal, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@decimal, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from decimal to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(double @double, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@double, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@double, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@double, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@double, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@double, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@double, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@double, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@double, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@double, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
#if NET10_0_OR_GREATER
                number = Unsafe.As<double, T>(ref @double);
#elif NET462_OR_GREATER
                number = (T)(object)@double;
#endif
                return true;
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@double, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from double to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryToNumeric<T>(float @float, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (TryConvert(@float, out sbyte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<sbyte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(byte))
            {
                if (TryConvert(@float, out byte res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(short))
            {
                if (TryConvert(@float, out short res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ushort))
            {
                if (TryConvert(@float, out ushort res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(int))
            {
                if (TryConvert(@float, out int res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(uint))
            {
                if (TryConvert(@float, out uint res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(long))
            {
                if (TryConvert(@float, out long res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (TryConvert(@float, out ulong res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(decimal))
            {
                if (TryConvert(@float, out decimal res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(double))
            {
                if (TryConvert(@float, out double res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            if (typeof(T) == typeof(float))
            {
                if (TryConvert(@float, out float res, rule))
                {
#if NET10_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
#elif NET462_OR_GREATER
                    number = (T)(object)res;
#endif
                    return true;
                }
                else { number = default; return false; }
            }

            throw new InvalidOperationException($"Unsupported conversion from float to {typeof(T)}");
        }
    }
}