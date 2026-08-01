using System;
using System.Runtime.CompilerServices;

namespace Atomtick.Common
{
    /// <summary>
    /// 具体类型 => 泛型
    /// </summary>
    public partial class NumericToNumeric
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<TSource, TTarget>(TSource source, out TTarget target, ConversionRule rule)
        {
            if (typeof(TSource) == typeof(TTarget))
            {
#if NET8_0_OR_GREATER
                target = Unsafe.As<TSource, TTarget>(ref source);
#elif NET462_OR_GREATER
                target = (TTarget)(object)source;
#endif
                return true;
            }

            if (typeof(TSource) == typeof(sbyte))
            {
#if NET8_0_OR_GREATER
                var @sbyte = Unsafe.As<TSource, sbyte>(ref source);
#elif NET462_OR_GREATER
                var @sbyte = (sbyte)(object)source;
#endif
                return Try(@sbyte, out target, rule);
            }
            if (typeof(TSource) == typeof(byte))
            {
#if NET8_0_OR_GREATER
                var @byte = Unsafe.As<TSource, byte>(ref source);
#elif NET462_OR_GREATER
                var @byte = (byte)(object)source;
#endif
                return Try(@byte, out target, rule);
            }
            if (typeof(TSource) == typeof(short))
            {
#if NET8_0_OR_GREATER
                var @short = Unsafe.As<TSource, short>(ref source);
#elif NET462_OR_GREATER
                var @short = (short)(object)source;
#endif
                return Try(@short, out target, rule);
            }
            if (typeof(TSource) == typeof(ushort))
            {
#if NET8_0_OR_GREATER
                var @ushort = Unsafe.As<TSource, ushort>(ref source);
#elif NET462_OR_GREATER
                var @ushort = (ushort)(object)source;
#endif
                return Try(@ushort, out target, rule);
            }
            if (typeof(TSource) == typeof(int))
            {
#if NET8_0_OR_GREATER
                var @int = Unsafe.As<TSource, int>(ref source);
#elif NET462_OR_GREATER
                var @int = (int)(object)source;
#endif
                return Try(@int, out target, rule);
            }
            if (typeof(TSource) == typeof(uint))
            {
#if NET8_0_OR_GREATER
                var @uint = Unsafe.As<TSource, uint>(ref source);
#elif NET462_OR_GREATER
                var @uint = (uint)(object)source;
#endif
                return Try(@uint, out target, rule);
            }
            if (typeof(TSource) == typeof(long))
            {
#if NET8_0_OR_GREATER
                var @long = Unsafe.As<TSource, long>(ref source);
#elif NET462_OR_GREATER
                var @long = (long)(object)source;
#endif
                return Try(@long, out target, rule);
            }
            if (typeof(TSource) == typeof(ulong))
            {
#if NET8_0_OR_GREATER
                var @ulong = Unsafe.As<TSource, ulong>(ref source);
#elif NET462_OR_GREATER
                var @ulong = (ulong)(object)source;
#endif
                return Try(@ulong, out target, rule);
            }
            if (typeof(TSource) == typeof(float))
            {
#if NET8_0_OR_GREATER
                var @float = Unsafe.As<TSource, float>(ref source);
#elif NET462_OR_GREATER
                var @float = (float)(object)source;
#endif
                return Try(@float, out target, rule);
            }
            if (typeof(TSource) == typeof(double))
            {
#if NET8_0_OR_GREATER
                var @double = Unsafe.As<TSource, double>(ref source);
#elif NET462_OR_GREATER
                var @double = (double)(object)source;
#endif
                return Try(@double, out target, rule);
            }
            if (typeof(TSource) == typeof(decimal))
            {
#if NET8_0_OR_GREATER
                var @decimal = Unsafe.As<TSource, decimal>(ref source);
#elif NET462_OR_GREATER
                var @decimal = (decimal)(object)source;
#endif
                return Try(@decimal, out target, rule);
            }
            target = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(sbyte @sbyte, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try((short)@sbyte, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@sbyte, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res); // 0 开销强制转换
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
                if (Try(@sbyte, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res); // 0 开销强制转换
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
        public static bool Try<T>(byte @byte, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@byte, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try((short)@byte, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@byte, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@byte, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@byte, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@byte, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@byte, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@byte, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@byte, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@byte, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@byte, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from byte to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(short @short, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@short, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@short, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try((int)@short, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@short, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@short, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@short, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@short, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@short, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@short, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@short, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@short, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from short to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(ushort @ushort, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@ushort, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@ushort, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@ushort, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try((int)@ushort, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@ushort, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@ushort, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@ushort, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@ushort, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@ushort, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@ushort, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@ushort, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from ushort to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(int @int, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@int, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@int, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@int, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@int, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try((long)@int, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@int, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@int, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@int, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@int, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@int, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@int, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from int to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(uint @uint, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@uint, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@uint, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@uint, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@uint, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@uint, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try((long)@uint, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@uint, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@uint, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@uint, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@uint, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@uint, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from uint to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(long @long, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@long, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@long, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@long, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@long, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@long, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@long, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
#if NET8_0_OR_GREATER
                number = Unsafe.As<long, T>(ref @long);
#elif NET462_OR_GREATER
                number = (T)(object)@long;
#endif
                return true;
            }

            if (typeof(T) == typeof(ulong))
            {
                if (Try(@long, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@long, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@long, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@long, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from long to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(ulong @ulong, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@ulong, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@ulong, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@ulong, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@ulong, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@ulong, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@ulong, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@ulong, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
#if NET8_0_OR_GREATER
                number = Unsafe.As<ulong, T>(ref @ulong);
#elif NET462_OR_GREATER
                number = (T)(object)@ulong;
#endif
                return true;
            }

            if (typeof(T) == typeof(decimal))
            {
                if (Try(@ulong, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@ulong, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@ulong, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from ulong to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(decimal @decimal, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@decimal, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@decimal, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@decimal, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@decimal, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@decimal, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@decimal, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@decimal, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@decimal, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
#if NET8_0_OR_GREATER
                number = Unsafe.As<decimal, T>(ref @decimal);
#elif NET462_OR_GREATER
                number = (T)(object)@decimal;
#endif
                return true;
            }

            if (typeof(T) == typeof(double))
            {
                if (Try(@decimal, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@decimal, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from decimal to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(double @double, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@double, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@double, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@double, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@double, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@double, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@double, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@double, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@double, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@double, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
#if NET8_0_OR_GREATER
                number = Unsafe.As<double, T>(ref @double);
#elif NET462_OR_GREATER
                number = (T)(object)@double;
#endif
                return true;
            }

            if (typeof(T) == typeof(float))
            {
                if (Try(@double, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from double to {typeof(T)}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Try<T>(float @float, out T number, ConversionRule rule)
        {
            if (typeof(T) == typeof(sbyte))
            {
                if (Try(@float, out sbyte res, rule))
                {
#if NET8_0_OR_GREATER
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
                if (Try(@float, out byte res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<byte, T>(ref res);
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
                if (Try(@float, out short res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<short, T>(ref res);
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
                if (Try(@float, out ushort res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ushort, T>(ref res);
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
                if (Try(@float, out int res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<int, T>(ref res);
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
                if (Try(@float, out uint res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<uint, T>(ref res);
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

            if (typeof(T) == typeof(long))
            {
                if (Try(@float, out long res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<long, T>(ref res);
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
                if (Try(@float, out ulong res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<ulong, T>(ref res);
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
                if (Try(@float, out decimal res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<decimal, T>(ref res);
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
                if (Try(@float, out double res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<double, T>(ref res);
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
                if (Try(@float, out float res, rule))
                {
#if NET8_0_OR_GREATER
                    number = Unsafe.As<float, T>(ref res);
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

            throw new InvalidOperationException($"Unsupported conversion from float to {typeof(T)}");
        }
    }
}
