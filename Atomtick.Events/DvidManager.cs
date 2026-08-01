using Atomtick.Common;
using SCADA.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Events
{
    public class DvidManager
    {

        private readonly DoubleKeyDictionary<string, long, DvidInstance> _dvidInstancesByNameAndId;

        public DvidManager()
        {
            _dvidInstancesByNameAndId = new DoubleKeyDictionary<string, long, DvidInstance>();
        }

        public void Register(params DvidDef[] dvidDefs)
        {
            _dvidInstancesByNameAndId.Add(
                dvidDefs.Select(dvidDef =>
                new DoubleKeyPairs<string, long, DvidInstance>(dvidDef.Name, dvidDef.Dvid, new DvidInstance()
                {
                    DvidDef = dvidDef,
                })).ToArray());
        }

        public void Update<T>(long dvid, T value) where T : IConvertible
        {
            if (!Update(_dvidInstancesByNameAndId.GetByKey2(dvid), value, out string errMsg))
            {
                throw new ArgumentException(errMsg);
            }
        }

        public void Update<T>(string name, T value) where T : IConvertible
        {
            if (!Update(_dvidInstancesByNameAndId.GetByKey1(name), value, out string errMsg))
            {
                throw new ArgumentException(errMsg);
            }
        }

        public bool Update<T>(DvidInstance dvidInstance, T value,out string errMsg) where T : IConvertible
        {
            switch(dvidInstance.DvidDef.DataType)
            {
                case SecsDataType.Boolean:
                    if (value is bool)
                    {

                        dvidInstance.BoolCurrentValue = (bool)(object)value;

                        dvidInstance.BoolCurrentValue = Unsafe.As<T,bool>(ref value);
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.I1:
                    if (value is sbyte)
                    {

                        NumericToNumeric.Try<T, sbyte>(value, out var sbyteValue, ConversionRule.CheckOverflow);
                        dvidInstance.LongCurrentValue = 
                        

                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.I2:
                    if (value is short)
                    {
                        dvidInstance.LongCurrentValue = (short)(object)value;

                        dvidInstance.LongCurrentValue = Unsafe.As<T, short>(ref value);
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.I4:
                    if (value is int)
                    {
                        dvidInstance.LongCurrentValue = (int)(object)value;


                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.I8:
                    if (value is long)
                    {
                        dvidInstance.LongCurrentValue = (long)(object)value;
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.U1:
                    if (value is byte)
                    {
                        dvidInstance.LongCurrentValue = (byte)(object)value;
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.U2:
                    if (value is ushort)
                    {
                        dvidInstance.LongCurrentValue = (ushort)(object)value;
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.U4:
                    if (value is uint)
                    {
                        dvidInstance.LongCurrentValue = (uint)(object)value;
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.U8:
                    if (value is ulong)
                    {
                        dvidInstance.LongCurrentValue = (long)(ulong)(object)value; // 注意：可能会丢失精度
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.F4:
                    if (value is float)
                    {
                        dvidInstance.DoubleCurrentValue = (float)(object)value;
                        errMsg = null;
                        return true;
                    }
                    break;
                case SecsDataType.F8:
                    if (value is double)
            }


            if(dvidInstance.DvidDef.DataType == SecsDataType.Boolean && value is bool boolValue)
            {
                dvidInstance.BoolCurrentValue = boolValue;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.I1 && value is sbyte i1Value)
            {
                dvidInstance.LongCurrentValue = i1Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.I2 && value is short i2Value)
            {
                dvidInstance.LongCurrentValue = i2Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.I4 && value is int i4Value)
            {
                dvidInstance.LongCurrentValue = i4Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.I8 && value is long i8Value)
            {
                dvidInstance.LongCurrentValue = i8Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.U1 && value is byte u1Value)
            {
                dvidInstance.LongCurrentValue = u1Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.U2 && value is ushort u2Value)
            {
                dvidInstance.LongCurrentValue = u2Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.U4 && value is uint u4Value)
            {
                dvidInstance.LongCurrentValue = u4Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.U8 && value is ulong u8Value)
            {
                dvidInstance.LongCurrentValue = (long)u8Value; // 注意：可能会丢失精度
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.F4 && value is float f4Value)
            {
                dvidInstance.DoubleCurrentValue = f4Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.F8 && value is double f8Value)
            {
                dvidInstance.DoubleCurrentValue = f8Value;
            }
            else if (dvidInstance.DvidDef.DataType == SecsDataType.ASCII && value is string strValue)
            {
                dvidInstance.String
        }

        public T Read<T>(long dvid)
        {
            return TryRead<T>(dvid, out var value, out string errMsg) ? value : throw new ArgumentException(errMsg);
        }

        public bool TryRead<T>(long dvid, out T value, out string errMsg)
        {
            if (_dvidInstancesById.TryGetValue(dvid, out var instance))
            {
                return TryRead<T>(instance, out value, out errMsg);
            }
            else
            {
                value = default;
                errMsg = null;
                return false;
            }
        }

        public T Read<T>(string name)
        {
            return TryRead<T>(name, out var value, out string errMsg) ? value : throw new ArgumentException(errMsg);
        }

        public bool TryRead<T>(string name, out T value, out string errMsg)
        {
            if (_dvidInstancesByName.TryGetValue(name, out var instance))
            {
                return TryRead<T>(instance, out value, out errMsg);
            }
            else
            {
                value = default;
                errMsg = null;
                return false;
            }
        }

        private bool TryRead<T>(DvidInstance dvidInstance, out T value, out string errMsg)
        {
            switch (dvidInstance.DvidDef.DataType)
            {
                case SecsDataType.Boolean:
                    if (typeof(T) != typeof(bool))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(bool)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)dvidInstance.BoolCurrentValue;
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    bool b = dvidInstance.BoolCurrentValue;
                    value = Unsafe.As<bool, T>(ref b); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.I1:
                    if (typeof(T) != typeof(sbyte))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(sbyte)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((sbyte)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte i1 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref i1); // 0 开销强制转换
                    errMsg = null;
                    return true;

#endif
                case SecsDataType.I2:
                    if (typeof(T) != typeof(short))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(short)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((short)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte i2 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref i2); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.I4:
                    if (typeof(T) != typeof(int))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(int)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((int)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte i4 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref i4); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.I8:
                    if (typeof(T) != typeof(long))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(long)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((long)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte i8 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref i8); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif

                case SecsDataType.U1:
                    if (typeof(T) != typeof(byte))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(byte)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((byte)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte u1 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref u1); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.U2:
                    if (typeof(T) != typeof(ushort))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(ushort)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((ushort)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte u2 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref u2); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.U4:
                    if (typeof(T) != typeof(uint))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(uint)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((uint)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte u4 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref u4); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.U8:
                    if (typeof(T) != typeof(ulong))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(ulong)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((ulong)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte u8 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref u8); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif

                case SecsDataType.F4:
                    if (typeof(T) != typeof(float))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(float)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((uint)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte f4 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref f4); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.F8:
                    if (typeof(T) != typeof(double))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(double)}, but got {typeof(T)}";
                        return false;
                    }
#if NET462_OR_GREATER
                    value = (T)(object)((ulong)dvidInstance.LongCurrentValue);
                    errMsg = null;
                    return true;
#elif NET8_0_OR_GREATER
                    sbyte f8 = (sbyte)dvidInstance.LongCurrentValue;
                    value = Unsafe.As<sbyte, T>(ref f8); // 0 开销强制转换
                    errMsg = null;
                    return true;
#endif
                case SecsDataType.ASCII:
                    if (typeof(T) != typeof(string))
                    {
                        value = default;
                        errMsg = $"Type mismatch: expected {typeof(string)}, but got {typeof(T)}";
                        return false;
                    }
                    value = (T)(object)dvidInstance.StringCurrentValue;
                    errMsg = null;
                    return true;

                default:
                    throw new InvalidOperationException($"Unsupported data type: {dvidInstance.DvidDef.DataType}");
            }
        }
    }
}
