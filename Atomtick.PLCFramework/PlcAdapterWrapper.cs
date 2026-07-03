using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using MoreLinq;
using SCADA.Common;
using SCADA.Common.Triggers;
using SCADA.ObjectModel;

namespace SCADA.PLCFramework
{
    // 改成多字典 io-name->object 或者是 io-name -> double or byte[] 来避免拆装箱,建议后者.
    public class PlcAdapterWrapper : DeviceBase, IDevice, IConnection, IPlcAccessor, IInterlockChecker
    {
        #region PLC RAW DATA

        private bool[] DIBlockValue;
        private byte[] AIBlockValue;
        private bool[] DOBlockValue;
        private byte[] AOBlockValue;
        private long DIBlockValueTimestamp;
        private long AIBlockValueTimestamp;
        private long DOBlockValueTimestamp;
        private long AOBlockValueTimestamp;

        #endregion PLC RAW DATA

        #region Primitive Type Value

        private readonly ConcurrentQueue<WriteData> _writeDataQueue;
        private readonly ConcurrentDictionary<long, bool> _writeIdSet;
        private readonly List<WriteData.Entity> _toWriteOutputEntities = new List<WriteData.Entity>();
        private readonly List<long> _toDeleteWriteId = new List<long>();
        private long _writeIdGenerator = long.MinValue;
        private int _snapshotIndex = 0;
        private volatile Dictionary<string, double> _currentIOValueSnapshot;
        private Dictionary<string, double>[] _IOValueSnapshotBuffer;

        #endregion Primitive Type Value

        private readonly Dictionary<int, F_TRIG> _triggers;
        private readonly FrozenDictionary<string, RegistItem> _registItems;
        private readonly IPlcAdapter _plcAdapter;
        private readonly PlcInfo _plcInfo;

        public long GetNextWriteId() => Interlocked.Increment(ref _writeIdGenerator);

        public PlcAdapterWrapper(PlcInfo plcInfo)
            : base(plcInfo.Module, plcInfo.Name)
        {
            _plcInfo = plcInfo;

            var diBlock = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling && x.Value.Type == RegisterType.DI);
            if (diBlock.Key != null && diBlock.Value != null)
            {
                DIBlockValue = new bool[diBlock.Value.Len];
            }
            var doBlock = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling && x.Value.Type == RegisterType.DO);
            if (doBlock.Key != null && doBlock.Value != null)
            {
                DOBlockValue = new bool[doBlock.Value.Len];
            }
            var aiBlock = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling && x.Value.Type == RegisterType.AI);
            if (aiBlock.Key != null && aiBlock.Value != null)
            {
                AIBlockValue = new byte[aiBlock.Value.Len];
            }
            var aoBlock = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling && x.Value.Type == RegisterType.AO);
            if (aoBlock.Key != null && aoBlock.Value != null)
            {
                AOBlockValue = new byte[aoBlock.Value.Len];
            }
            DIBlockValueTimestamp = long.MinValue;
            AIBlockValueTimestamp = long.MinValue;
            DOBlockValueTimestamp = long.MaxValue;
            AOBlockValueTimestamp = long.MinValue;

            _writeDataQueue = new ConcurrentQueue<WriteData>();
            _writeIdSet = new ConcurrentDictionary<long, bool>();
            _IOValueSnapshotBuffer = new Dictionary<string, double>[_plcInfo.IOValueBufferCount];

            Dictionary<string, RegistItem> _dict = new Dictionary<string, RegistItem>();
            _plcInfo.DIs.ForEach(x => _dict.Add(x.Key, x.Value));
            _plcInfo.DOs.ForEach(x => _dict.Add(x.Key, x.Value));
            _plcInfo.AIs.ForEach(x => _dict.Add(x.Key, x.Value));
            _plcInfo.AOs.ForEach(x => _dict.Add(x.Key, x.Value));
            _registItems = _dict.ToFrozenDictionary();

            _plcAdapter = Assembly.LoadFrom(plcInfo.Assembly).GetType(plcInfo.Class).GetConstructor([typeof(string)]).Invoke([plcInfo.Address]) as IPlcAdapter;

            // 启动定时读写PLC缓存到上位机线程
            Thread poller = new(new ThreadStart(PollingWorker)) { IsBackground = true, Priority = ThreadPriority.AboveNormal };
            poller.Start();
        }

        private RegistItem GetRegistItem(string name) => _registItems.TryGetValue(name, out RegistItem value) ? value : null;

        #region Read

        private ReadResult<T> Read<T>(string name, Dictionary<string, double> snapshot)
            where T : IConvertible, IComparable
        {
            long timestamp;
            T value;

            var item = GetRegistItem(name);
            Type t = typeof(T);
            switch (item.Type)
            {
                case ValueType.@bool:
                    if (t != typeof(bool))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a bool in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    bool boolValue = _currentIOValueSnapshot[name] == 1;
                    value = Unsafe.As<bool, T>(ref boolValue);
                    break;

                case ValueType.int8:
                    if (t != typeof(sbyte))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a int8 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    sbyte int8Value = Convert.ToSByte(snapshot[name]);
                    value = Unsafe.As<sbyte, T>(ref int8Value);
                    break;

                case ValueType.int16:
                    if (t != typeof(short))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a int16 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    short int16Value = Convert.ToInt16(snapshot[name]);
                    value = Unsafe.As<short, T>(ref int16Value);
                    break;

                case ValueType.int32:
                    if (t != typeof(int))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a int32 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    int int32Value = Convert.ToInt32(snapshot[name]);
                    value = Unsafe.As<int, T>(ref int32Value);
                    break;

                case ValueType.int64:
                    if (t != typeof(long))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a int64 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    long int64Value = Convert.ToInt64(snapshot[name]);
                    value = Unsafe.As<long, T>(ref int64Value);
                    break;

                case ValueType.uint8:
                    if (t != typeof(byte))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a uint8 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    byte uint8Value = Convert.ToByte(snapshot[name]);
                    value = Unsafe.As<byte, T>(ref uint8Value);
                    break;

                case ValueType.uint16:
                    if (t != typeof(ushort))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a uint16 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    ushort uint16Value = Convert.ToUInt16(snapshot[name]);
                    value = Unsafe.As<ushort, T>(ref uint16Value);
                    break;

                case ValueType.uint32:
                    if (t != typeof(uint))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a uint32 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    uint uint32Value = Convert.ToUInt32(snapshot[name]);
                    value = Unsafe.As<uint, T>(ref uint32Value);
                    break;

                case ValueType.uint64:
                    if (t != typeof(ulong))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a uint64 in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    ulong uint64Value = Convert.ToUInt64(snapshot[name]);
                    value = Unsafe.As<ulong, T>(ref uint64Value);
                    break;

                case ValueType.@float:
                    if (t != typeof(float))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a float in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    float floatValue = Convert.ToSingle(snapshot[name]);
                    value = Unsafe.As<float, T>(ref floatValue);
                    break;

                case ValueType.@double:
                    if (t != typeof(double))
                    {
                        throw new InvalidCastException(
                            $"{name}'s type is configured as a double in the (module:{_plcInfo.Module}-name:{_plcInfo.Name}-address:{_plcInfo.Address}) file instead of a {t.Name}."
                        );
                    }
                    double doubleValue = snapshot[name];
                    value = Unsafe.As<double, T>(ref doubleValue);
                    break;

                default:
                    value = default;
                    break;
            }

            var block = _plcInfo.Blocks[item.BlockId];
            if (block.Type == RegisterType.DI)
                timestamp = BitConverter.DoubleToInt64Bits(["diBlockUpdateValuesTimestamp"]);
            else if (block.Type == RegisterType.DO)
                timestamp = AIBlockValueTimestamp;
            else if (block.Type == RegisterType.AI)
                timestamp = DOBlockValueTimestamp;
            else if (block.Type == RegisterType.AO)
                timestamp = AOBlockValueTimestamp;
            else
                timestamp = long.MinValue; // 几百年前的数据,上位机读取后肯定能查到是过期数据.
            return new ReadResult<T>(timestamp, value);
        }

        public ReadResult<T> Read<T>(string name)
            where T : IConvertible, IComparable
        {
            return Read<T>(name, _currentIOValueSnapshot);
        }

        public ReadResult<T1, T2> Read<T1, T2>(string name1, string name2)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
        {
            var snapshot = _currentIOValueSnapshot;
            return new ReadResult<T1, T2>(Read<T1>(name1, snapshot), Read<T2>(name2, snapshot));
        }

        public ReadResult<T1, T2, T3> Read<T1, T2, T3>(string name1, string name2, string name3)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
            where T3 : IConvertible, IComparable
        {
            var snapshot = _currentIOValueSnapshot;
            return new ReadResult<T1, T2, T3>(Read<T1>(name1, snapshot), Read<T2>(name2, snapshot), Read<T3>(name3, snapshot));
        }

        public ReadResult<T1, T2, T3, T4> Read<T1, T2, T3, T4>(string name1, string name2, string name3, string name4)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
            where T3 : IConvertible, IComparable
            where T4 : IConvertible, IComparable
        {
            var snapshot = _currentIOValueSnapshot;
            return new ReadResult<T1, T2, T3, T4>(Read<T1>(name1, snapshot), Read<T2>(name2, snapshot), Read<T3>(name3, snapshot), Read<T4>(name4, snapshot));
        }

        public ReadResult<T1, T2, T3, T4, T5> Read<T1, T2, T3, T4, T5>(string name1, string name2, string name3, string name4, string name5)
            where T1 : IConvertible, IComparable
            where T2 : IConvertible, IComparable
            where T3 : IConvertible, IComparable
            where T4 : IConvertible, IComparable
            where T5 : IConvertible, IComparable
        {
            var snapshot = _currentIOValueSnapshot;
            return new ReadResult<T1, T2, T3, T4, T5>(Read<T1>(name1, snapshot), Read<T2>(name2, snapshot), Read<T3>(name3, snapshot), Read<T4>(name4, snapshot), Read<T5>(name5, snapshot));
        }

        public ReadResult Read<T>(params string[] names)
        {
            var result = new ReadResult();
        }

        #endregion Read

        #region Write

        public long Write(params (string name, object value)[] nameValues)
        {
            var id = GetNextWriteId();
            _writeIdSet.TryAdd(id, false);
            _writeDataQueue.Enqueue(new WriteData(id, [name1, name2, name3], [value1, value2]));
            return id;
        }

        public long Write((string name, object value) nameValuePair) { }

        public long Write((string name, object value) nameValuePair1, (string name, object value) nameValuePair2) { }

        public long Write((string name, object value) nameValuePair1, (string name, object value) nameValuePair2, (string name, object value) nameValuePair3) { }

        #endregion Write

        public bool IsWriteCompleted(long id) => !_writeIdSet.ContainsKey(id);

        private void PollingWorker()
        {
            while (true)
            {
                // READ AIs
                KeyValuePair<string, Block> aiBlockPair = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling == true && x.Value.Type == "ai");
                if (aiBlockPair.Key != null && aiBlockPair.Value != null)
                {
                    var aiBlock = aiBlockPair.Value;
                    AIBlockValueTimestamp = Stopwatch.GetTimestamp(); // 故意让值更旧
                    var bytes = _plcAdapter.ReadAIs(aiBlock.StartAddr, aiBlock.Len);
                    bytes.CopyTo(AIBlockValue);
                }
                // READ DIs
                KeyValuePair<string, Block> diBlockPair = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling == true && x.Value.Type == "di");
                if (diBlockPair.Key != null && diBlockPair.Value != null)
                {
                    var diBlock = diBlockPair.Value;
                    DIBlockValueTimestamp = Stopwatch.GetTimestamp(); // 故意让值更旧
                    var bools = _plcAdapter.ReadDIs(diBlock.StartAddr, diBlock.Len);
                    bools.CopyTo(DIBlockValue);
                }

                var snapshotBuffer = _IOValueSnapshotBuffer[_snapshotIndex++];
                snapshotBuffer["diBlockUpdateValuesTimestamp"] = BitConverter.Int64BitsToDouble(DIBlockValueTimestamp);
                snapshotBuffer["aiBlockUpdateValuesTimestamp"] = BitConverter.Int64BitsToDouble(AIBlockValueTimestamp);

                // 解析DI和AI地址到字典
                foreach (var diItem in _plcInfo.DIs)
                {
                    bool value;
                    var di = diItem.Value;
                    var block = _plcInfo.Blocks[di.BlockId];
                    if (block.Type == "di")
                    {
                        value = DIBlockValue[di.ByteIndex];
                    }
                    else if (block.Type == "ai")
                    {
                        var @byte = AIBlockValue[di.ByteIndex];
                        if (di.BitIndex < 0)
                        {
                            value = @byte == 1;
                        }
                        else
                        {
                            value = SCADA.Common.Utility.GetBitValue(@byte, di.BitIndex);
                        }
                    }
                    else
                        throw new Exception();

                    snapshotBuffer[di.Name] = Convert.ToDouble(value);
                }

                foreach (var aiItem in _plcInfo.AIs)
                {
                    double value;
                    var ai = aiItem.Value;
                    var block = _plcInfo.Blocks[(ai.BlockId)];
                }

                // check interlock here.

                var doBlockValueChanged = false;
                var aoBlockValueChanged = false;

                #region 取待写输出

                // 从待写队列取出本次可写的PLC地址.
                // 一直取到地址出现重复且重复的地址对应的待写值与更早待写的值不相等为止.
                // 这样做的好处是如果逻辑层出现死循环一直高频往某个PLC地址写同样的一个值时,多次重复写入相同值会合并成单次,
                // 且不会打断从队列中继续取可批量写入的其他待写地址,保证了单次批量写入的地址的数量最大化,让效率最大化.
                _toDeleteWriteId.Clear();
                _toWriteOutputEntities.Clear();
                while (_writeDataQueue.TryPeek(out var writeData))
                {
                    bool isPresent = false;
                    foreach (var item in writeData.Entities)
                    {
                        var index = _toWriteOutputEntities.FindIndex(x => x.Name == item.Name);
                        if (index != -1 && _toWriteOutputEntities[index].Value != item.Value)
                        {
                            isPresent = true;
                            break;
                        }
                    }
                    if (isPresent == false)
                    {
                        writeData.Entities.ForEach(x => _toWriteOutputEntities.Add(new WriteData.Entity() { Name = x.Name, Value = x.Value }));
                        if (_writeDataQueue.TryDequeue(out var removed))
                            _toDeleteWriteId.Add(removed.ID);
                    }
                    else
                        break;
                }

                #endregion 取待写输出

                #region 把待写地址转换成字节,刷新到Block值数组中

                if (_toWriteOutputEntities.Count > 0)
                {
                    foreach (var entity in _toWriteOutputEntities)
                    {
                        var registItem = GetRegistItem(entity.Name);
                        var block = _plcInfo.Blocks[registItem.BlockId];
                        switch (registItem.Type)
                        {
                            case ValueType.@bool:
                                if (block.Type == "di") { }
                                break;

                            case ValueType.int8:
                                break;

                            case ValueType.int16:
                                break;

                            case ValueType.int32:
                                break;

                            case ValueType.int64:
                                break;

                            case ValueType.uint8:
                                break;

                            case ValueType.uint16:
                                break;

                            case ValueType.uint32:
                                break;

                            case ValueType.uint64:
                                break;

                            case ValueType.@float:
                                break;

                            case ValueType.@double:
                                break;

                            default:
                                break;
                        }
                    }
                }

                #endregion 把待写地址转换成字节,刷新到Block值数组中

                #region 把Block值数组写入到PLC里面

                bool isWriteDOBlockSuccessed = true;
                bool isWriteAOBlockSuccessed = true;

                if (doBlockValueChanged)
                {
                    var doBlock = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling && x.Value.Type == "do").Value;
                    isWriteDOBlockSuccessed = _plcAdapter.WriteDOs(doBlock.StartAddr, DOBlockValue);
                }
                if (aoBlockValueChanged)
                {
                    var aoBlock = _plcInfo.Blocks.SingleOrDefault(x => x.Value.Polling && x.Value.Type == "ao").Value;
                    isWriteAOBlockSuccessed = _plcAdapter.WriteAOs(aoBlock.StartAddr, AOBlockValue);
                }

                if (isWriteDOBlockSuccessed == false || isWriteAOBlockSuccessed == false)
                {
                    _toDeleteWriteId.ForEach(x => _writeIdSet.TryRemove(x, out _));
                    _toDeleteWriteId.Clear();
                    _toWriteOutputEntities.Clear();
                }

                #endregion 把Block值数组写入到PLC里面

                Thread.Sleep(10);
            }
        }

        #region Interlock

        IReadOnlyDictionary<InterlockAction, InterlockLimit[]> IInterlockChecker.GetAllActions()
        {
            return _plcInfo.ActionLimitsDict;
        }

        IEnumerable<InterlockAction> IInterlockChecker.GetAllInterLockActions()
        {
            return _plcInfo.ActionLimitsDict.Keys.Where(x => !x.ByPass);
        }

        IEnumerable<InterlockAction> IInterlockChecker.GetAllBypassActions()
        {
            return _plcInfo.ActionLimitsDict.Keys.Where(x => x.ByPass);
        }

        IEnumerable<InterlockLimit> IInterlockChecker.GetAllLimits(InterlockAction action)
        {
            return _plcInfo.ActionLimitsDict[action];
        }

        IEnumerable<InterlockLimit> IInterlockChecker.GetAllInterLockLimits(InterlockAction action)
        {
            return _plcInfo.ActionLimitsDict[action].Where(x => !x.ByPass);
        }

        IEnumerable<InterlockLimit> IInterlockChecker.GetAllBypassLimits(InterlockAction action)
        {
            return _plcInfo.ActionLimitsDict[action].Where(x => x.ByPass);
        }

        void IInterlockChecker.BypassInterLock(InterlockAction action)
        {
            action.ByPass = true;
        }

        void IInterlockChecker.BypassInterLock(InterlockAction action, InterlockLimit limit)
        {
            var target = _plcInfo.ActionLimitsDict[action].SingleOrDefault(x => x.Equals(limit));
            if (target != null)
            {
                target.ByPass = true;
            }
        }

        void IInterlockChecker.RestoreInterlock(InterlockAction action)
        {
            action.ByPass = false;
        }

        void IInterlockChecker.RestoreInterlock(InterlockAction action, InterlockLimit limit)
        {
            var target = _plcInfo.ActionLimitsDict[action].SingleOrDefault(x => x.Equals(limit));
            if (target != null)
            {
                target.ByPass = false;
            }
        }

        IEnumerable<string> IInterlockChecker.Monitor()
        {
            foreach (var action in _plcInfo.ActionLimitsDict)
            {
                if (action.Key.ByPass == false && action.Key.Reserve)
                {
                    foreach (var limit in action.Value)
                    {
                        if (limit.ByPass == false)
                        {
                            var trigger = _triggers[action.Key.GetHashCode() + limit.GetHashCode()];
                            trigger.CLK = Read<bool>(limit.Name).Value != limit.LimitValue;
                            if (trigger.Q)
                            {
                                Write((action.Key.Name, !action.Key.ActionValue));
                                yield return "";
                                break;
                            }
                        }
                    }
                }
            }
        }

        bool IInterlockChecker.CanDO(InterlockAction action, out InterlockLimit interlockLimit)
        {
            interlockLimit = null;
            bool canDo = true;
            if (action.ByPass)
            {
                return canDo;
            }
            foreach (var limit in _plcInfo.ActionLimitsDict[action])
            {
                if (!limit.ByPass)
                {
                    if (Read<bool>(limit.Name).Value != limit.LimitValue)
                    {
                        canDo = false;
                        interlockLimit = limit;
                        break;
                    }
                }
            }
            return canDo;
        }

        #endregion Interlock

        private T ConvertDoubleToType<T>(ValueType valueType, double value)
        {
            switch (valueType)
            {
                case ValueType.@double:
                    return Unsafe.As<double, T>(ref value);

                case ValueType.@float:
                    var @float = Convert.ToSingle(value);
                    return Unsafe.As<float, T>(ref @float);

                case ValueType.int8:
                    var int8 = Convert.ToSByte(value);
                    return Unsafe.As<sbyte, T>(ref int8);

                case ValueType.int16:
                    var int16 = Convert.ToInt16(value);
                    return Unsafe.As<short, T>(ref int16);

                case ValueType.int32:
                    var int32 = Convert.ToInt32(value);
                    return Unsafe.As<int, T>(ref int32);

                case ValueType.int64:
                    var int64 = Convert.ToInt64(value);
                    return Unsafe.As<long, T>(ref int64);

                case ValueType.uint8:
                    var uint8 = Convert.ToByte(value);
                    return Unsafe.As<byte, T>(ref uint8);

                case ValueType.uint16:
                    var uint16 = Convert.ToUInt16(value);
                    return Unsafe.As<ushort, T>(ref uint16);

                case ValueType.uint32:
                    var uint32 = Convert.ToUInt32(value);
                    return Unsafe.As<uint, T>(ref uint32);

                case ValueType.uint64:
                    var uint64 = Convert.ToUInt64(value);
                    return Unsafe.As<ulong, T>(ref uint64);

                case ValueType.@bool:
                    var @bool = value == 1;
                    return Unsafe.As<bool, T>(ref @bool);
            }
            return default;
        }

        private double ConvertTypeToDouble(bool value)
        {
            return value ? 1 : 0;
        }

        private double ConvertTypeToDouble(float value)
        {
            return value;
        }

        private double ConvertTypeToDouble(sbyte value)
        {
            return value;
        }

        private double ConvertTypeToDouble(short value)
        {
            return value;
        }

        private double ConvertTypeToDouble(int value)
        {
            return value;
        }

        private double ConvertTypeToDouble(long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }
    }
}
