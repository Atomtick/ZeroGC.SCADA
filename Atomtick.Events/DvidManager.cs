using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Atomtick.Common;
using SCADA.Common;

namespace Atomtick.Events
{
    public class DvidManager
    {
        private readonly DoubleKeyDictionary<string, long, DvidInstance> _dvidInstancesByNameAndId;

        private DvidManager()
        {
            _dvidInstancesByNameAndId = new DoubleKeyDictionary<string, long, DvidInstance>();
        }

        public static DvidManager Instance { get; } = new DvidManager();

        public void Register(IEnumerable<DvidDef> dvidDefs)
        {
            List<DvidInstance> dvidDefList = new List<DvidInstance>(8);
            foreach (var dvidDef in dvidDefs)
            {
                var dvidInstance = new DvidInstance(dvidDef);
                if (dvidDef.DataType == SecsDataType.Boolean)
                {
                    dvidInstance.BoolCurrentValue = (bool)dvidDef.InitialValue;
                }
                else if (dvidDef.DataType == SecsDataType.ASCII)
                {
                    dvidInstance.StringCurrentValue = (string)dvidDef.InitialValue;
                }
                else if (dvidDef.DataType == SecsDataType.F4 || dvidDef.DataType == SecsDataType.F8)
                {
                    dvidInstance.DoubleCurrentValue = Convert.ToDouble(dvidDef.InitialValue);
                }
                else
                {
                    dvidInstance.LongCurrentValue = Convert.ToInt64(dvidDef.InitialValue);
                }
                dvidDefList.Add(dvidInstance);
            }
            bool ok = _dvidInstancesByNameAndId.Add(
                dvidDefList.Select(x => new DoubleKeyValuePairs<string, long, DvidInstance>(x.DvidDef.Name, x.DvidDef.Dvid, x)),
                out var duplicateKey1,
                out var duplicateKey2
            );
            if (ok == false)
            {
                throw new ArgumentException($"Duplicate DVID definitions found. Duplicate name: '{duplicateKey1}', Duplicate ID: '{duplicateKey2}'");
            }
        }

        public bool TryUpdate<T>(long dvid, T value, out string errMsg)
            where T : IConvertible
        {
            if (_dvidInstancesByNameAndId.GetByKey2(dvid, out var dvidInstance))
                return TryUpdate(dvidInstance, value, out errMsg);
            errMsg = $"DVID with ID '{dvid}' not found.";
            return false;
        }

        public void TryUpdate<T>(long dvid, T value)
            where T : IConvertible
        {
            if (TryUpdate(dvid, value, out var errMsg) == false)
            {
                throw new ArgumentException(errMsg);
            }
        }

        public bool TryUpdate<T>(string name, T value, out string errMsg)
            where T : IConvertible
        {
            if (_dvidInstancesByNameAndId.GetByKey1(name, out var dvidInstance))
                return TryUpdate(dvidInstance, value, out errMsg);
            errMsg = $"DVID with name '{name}' not found.";
            return false;
        }

        public void TryUpdate<T>(string name, T value)
            where T : IConvertible
        {
            if (TryUpdate(name, value, out var errMsg) == false)
            {
                throw new ArgumentException(errMsg);
            }
        }

        private bool IsInteger<T>(T value)
        {
            return value is int || value is short || value is long || value is byte || value is ushort || value is uint || value is ulong || value is sbyte;
        }

        private bool IsDecimal<T>(T value)
        {
            return value is double || value is float || value is decimal;
        }

        private bool TryUpdate<T>(DvidInstance dvidInstance, T value, out string errMsg)
            where T : IConvertible
        {
            switch (dvidInstance.DvidDef.DataType)
            {
                case SecsDataType.Boolean:
                    if (value is bool @bool)
                    {
                        dvidInstance.BoolCurrentValue = @bool;
                        errMsg = null;
                        return true;
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected {typeof(bool)}, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.I1:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, sbyte>(value, out var sbyteValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = sbyteValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(sbyte)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.I2:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, short>(value, out var shortValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = shortValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(short)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.I4:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, int>(value, out var intValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = intValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(int)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.I8:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, long>(value, out var longValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = longValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(long)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.U1:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, byte>(value, out var byteValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = byteValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(byte)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.U2:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, ushort>(value, out var ushoryValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = ushoryValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(ushort)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.U4:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, uint>(value, out var uintValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = uintValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(uint)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.U8:
                    if (IsInteger(value))
                    {
                        if (NumericToNumeric.Try<T, long>(value, out var longValue, ConversionRule.CheckOverflow))
                        {
                            dvidInstance.LongCurrentValue = longValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(long)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an integer type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.F4:
                    if (IsDecimal(value))
                    {
                        if (NumericToNumeric.Try<T, float>(value, out var floatValue, ConversionRule.CheckOverflow | ConversionRule.CheckPrecision))
                        {
                            dvidInstance.DoubleCurrentValue = floatValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(float)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an decimal type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.F8:
                    if (IsDecimal(value))
                    {
                        if (NumericToNumeric.Try<T, float>(value, out var doubleValue, ConversionRule.CheckOverflow | ConversionRule.CheckPrecision))
                        {
                            dvidInstance.DoubleCurrentValue = doubleValue;
                            errMsg = null;
                            return true;
                        }
                        else
                        {
                            errMsg = $"Value {value} is out of range for type {typeof(double)}";
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected an decimal type, but got {typeof(T)}";
                        return false;
                    }
                case SecsDataType.ASCII:
                    if (value is string strValue)
                    {
                        dvidInstance.StringCurrentValue = strValue;
                        errMsg = null;
                        return true;
                    }
                    else
                    {
                        errMsg = $"Type mismatch: expected {typeof(string)}, but got {typeof(T)}";
                        return false;
                    }

                default:
                    throw new InvalidOperationException($"Unsupported data type: {dvidInstance.DvidDef.DataType}");
            }
        }

        public T Read<T>(long dvid)
        {
            return Read<T>(dvid, out var value, out string errMsg) ? value : throw new ArgumentException(errMsg);
        }

        public bool Read<T>(long dvid, out T value, out string errMsg)
        {
            if (_dvidInstancesByNameAndId.GetByKey2(dvid, out var dvidInstance))
                return TryRead<T>(dvidInstance, out value, out errMsg);
            errMsg = $"DVID with name '{dvid}' not found.";
            value = default;
            return false;
        }

        public T Read<T>(string name)
        {
            return Read<T>(name, out var value, out string errMsg) ? value : throw new ArgumentException(errMsg);
        }

        public bool Read<T>(string name, out T value, out string errMsg)
        {
            if (_dvidInstancesByNameAndId.GetByKey1(name, out var dvidInstance))
                return TryRead<T>(dvidInstance, out value, out errMsg);
            errMsg = $"DVID with name '{name}' not found.";
            value = default;
            return false;
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
