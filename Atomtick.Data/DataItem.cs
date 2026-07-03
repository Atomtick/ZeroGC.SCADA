using ProtoBuf.WellKnownTypes;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SCADA.Data
{

    public class DataItem : IBoolDataItem, ILongDataItem, IDoubleDataItem, IStringDataItem, IObjectDataItem
    {
        internal bool? BoolValue;
        internal Data Data;
        internal DataPurpose DataItemPurpose;
        internal DataType DataItemType;
        internal double? DoubleValue;
        internal long? LongValue;
        internal string Name;
        internal object ObjectValue;
        internal string StringValue;
        internal long Timestamp;
        private long _version;

        public bool? Get(out long timestamp)
        {
            return Get<bool?>(out timestamp);
        }

        long? ILongDataItem.Get(out long timestamp)
        {
            return Get<long?>(out timestamp);
        }

        double? IDoubleDataItem.Get(out long timestamp)
        {
            return Get<double?>(out timestamp);
        }

        string IStringDataItem.Get(out long timestamp)
        {
            return Get<string>(out timestamp);
        }

        object IObjectDataItem.Get(out long timestamp)
        {
            return Get<object>(out timestamp);
        }

        void IBoolDataItem.Update(bool? value, long timestamp)
        {
            if (DataItemType != DataType.Bool)
            {
                throw new InvalidOperationException();
            }
            if (value != BoolValue)
            {
                Interlocked.Increment(ref _version);
                Thread.MemoryBarrier();

                BoolValue = value;
                Timestamp = timestamp;

                Thread.MemoryBarrier();
                Interlocked.Increment(ref _version);

                Data.Record(Name, value, timestamp);
            }
        }

        void ILongDataItem.Update(long? value, long timestamp)
        {
            if (DataItemType != DataType.Long)
            {
                throw new InvalidOperationException();
            }
            if (value != LongValue)
            {
                Interlocked.Increment(ref _version);
                Thread.MemoryBarrier();

                LongValue = value;
                Timestamp = timestamp;

                Thread.MemoryBarrier();
                Interlocked.Increment(ref _version);

                Data.Record(Name, value, timestamp);
            }
        }

        void IDoubleDataItem.Update(double? value, long timestamp)
        {
            if (DataItemType != DataType.Double)
            {
                throw new InvalidOperationException();
            }
            if (value != LongValue)
            {
                Interlocked.Increment(ref _version);
                Thread.MemoryBarrier();

                DoubleValue = value;
                Timestamp = timestamp;

                Thread.MemoryBarrier();
                Interlocked.Increment(ref _version);
                Data.Record(Name, value, timestamp);
            }
        }

        void IStringDataItem.Update(string value, long timestamp)
        {
            if (DataItemType != DataType.String512)
            {
                throw new InvalidOperationException();
            }
            if (value != StringValue)
            {
                Interlocked.Increment(ref _version);
                Thread.MemoryBarrier();

                StringValue = value;
                Timestamp = timestamp;

                Thread.MemoryBarrier();
                Interlocked.Increment(ref _version);

                Data.Record(Name, value, timestamp);
            }
        }

        public void Update(object value, long timestamp)
        {
            if (DataItemType != DataType.Object2K)
            {
                throw new InvalidOperationException();
            }
            if (!Equals(value, ObjectValue))
            {
                Interlocked.Increment(ref _version);
                Thread.MemoryBarrier();

                ObjectValue = value;
                Timestamp = timestamp;

                Thread.MemoryBarrier();
                Interlocked.Increment(ref _version);
                Data.Record(Name, value, timestamp);
            }
        }

        private T Get<T>(out long timestamp)
        {
            while (true)
            {
                long v1 = Volatile.Read(ref _version);

                // 奇数：写线程正在更新
                if ((v1 & 1) != 0)
                {
                    // 【核心优化】：直接调用底层方法。
                    // 这里的 5 代表让 CPU 执行约 5 次 NOP/PAUSE 操作。
                    // 这个数字可以根据你的硬件微调（通常 5~10 足够写线程完成那几个引用的赋值）。
                    // 它绝对不会让出操作系统的线程时间片。
                    Thread.SpinWait(5);
                    continue;
                }

                Thread.MemoryBarrier();

                // 极速拷贝

                timestamp = Timestamp;
                T value = default;
                if (typeof(T) == typeof(bool?))
                {
                    value = (T)(object)BoolValue;
                }
                else if (typeof(T) == typeof(long?))
                {
                    value = (T)(object)LongValue;
                }
                else if (typeof(T) == typeof(double?))
                {
                    value = (T)(object)DoubleValue;
                }
                else if (typeof(T) == typeof(string))
                {
                    value = (T)(object)StringValue;
                }
                else if (typeof(T) == typeof(object))
                {
                    value = (T)(object)ObjectValue;
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type");
                }

                Thread.MemoryBarrier();

                long v2 = Volatile.Read(ref _version);

                if (v1 == v2)
                {
                    return value;
                }

                // 脏数据重试时，同样只进行 CPU 级别的极短暂停
                Thread.SpinWait(5);
            }
        }
    }
}