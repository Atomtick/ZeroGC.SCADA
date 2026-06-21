using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCADA.Common
{
    /// <summary>
    /// 高性能、无锁、单生产者单消费者 (SPSC) 环形缓冲区。
    /// 兼容 .NET Framework 4.6.2 到 .NET 10。
    /// 强制要求：一个线程专门负责写入，另一个线程专门负责读取。绝对不能多线程同时写或同时读。
    /// </summary>
    /// <typeparam name="T">元素类型（推荐使用非托管 Struct 以获得极致性能）</typeparam>
    public sealed class SpScRingBuffer<T>
    {
        private readonly T[] _buffer;
        private readonly int _mask;
        private readonly int _capacity;

        // 使用 padding 防止伪共享 (False Sharing)，将读写指针隔离开到不同的 CPU 缓存行 (Cache Line)
        // 现代 CPU 的 Cache Line 通常为 64 bytes 或 128 bytes
        [StructLayout(LayoutKind.Explicit, Size = 128)]
        private struct Pointers
        {
            [FieldOffset(0)]
            public int Head; // 生产者写入位置

            [FieldOffset(64)]
            public int Tail; // 消费者读取位置
        }

        private Pointers _ptrs;

        /// <summary>
        /// 初始化环形缓冲区。容量会被自动向上取整为 2 的幂。
        /// </summary>
        /// <param name="minCapacity">期望的最小容量</param>
        public SpScRingBuffer(int minCapacity)
        {
            // 强制容量为 2 的幂，以便用位运算 (&) 替代低效的取模运算 (%)
            _capacity = RoundUpToPowerOf2(minCapacity);
            _mask = _capacity - 1;
            _buffer = new T[_capacity];
            _ptrs = new Pointers();
        }

        /// <summary>
        /// 写入单个元素 (Zero-Allocation)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryEnqueue(T item)
        {
            int currentHead = _ptrs.Head;
            int currentTail = Volatile.Read(ref _ptrs.Tail);

            // unchecked 保证指针溢出 (超过 int.MaxValue) 时计算依然正确
            int count;
            unchecked
            {
                count = currentHead - currentTail;
            }

            if (count >= _capacity)
            {
                return false;
            }

            // 位与运算替代取模
            _buffer[currentHead & _mask] = item;
            // 写入完成，更新 Head
            unchecked
            {
                Volatile.Write(ref _ptrs.Head, currentHead + 1);
            }
            return true;
        }

        /// <summary>
        /// 读取单个元素 (Zero-Allocation)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryDequeue(out T item)
        {
            int currentTail = _ptrs.Tail;
            int currentHead = Volatile.Read(ref _ptrs.Head);

            if (currentTail == currentHead)
            {
                item = default;
                return false;
            }

            int tailIndex = currentTail & _mask;
            item = _buffer[tailIndex];

            // 编译时常量折叠判定，避免 GC 内存泄漏
            if (TypeTraits.NeedsClearing)
            {
                _buffer[tailIndex] = default;
            }
            // 读取完成，释放空间
            unchecked
            {
                Volatile.Write(ref _ptrs.Tail, currentTail + 1);
            }
            return true;
        }

        /// <summary>
        /// 批量连续写入 (针对高频字节流或结构体数组深度优化)
        /// </summary>
        public int Write(ReadOnlySpan<T> source)
        {
            int currentHead = _ptrs.Head;
            int currentTail = Volatile.Read(ref _ptrs.Tail);

            int count;
            unchecked
            {
                count = currentHead - currentTail;
            }

            int availableSpace = _capacity - count;
            int writeCount = Math.Min(source.Length, availableSpace);

            if (writeCount == 0)
                return 0;

            int headIndex = currentHead & _mask;
            int rightLength = _capacity - headIndex;

            if (writeCount <= rightLength)
            {
                // 一次性顺序写入
                source.Slice(0, writeCount).CopyTo(new Span<T>(_buffer, headIndex, writeCount));
            }
            else
            {
                // 绕回 (Wrap-around) 写入
                source.Slice(0, rightLength).CopyTo(new Span<T>(_buffer, headIndex, rightLength));
                source.Slice(rightLength, writeCount - rightLength).CopyTo(new Span<T>(_buffer, 0, writeCount - rightLength));
            }
            unchecked
            {
                Volatile.Write(ref _ptrs.Head, currentHead + writeCount);
            }
            return writeCount;
        }

        /// <summary>
        /// 批量连续读取 (配合 Span 实现零拷贝处理)
        /// </summary>
        public int Read(Span<T> destination)
        {
            int currentTail = _ptrs.Tail;
            int currentHead = Volatile.Read(ref _ptrs.Head);

            int availableItems;
            unchecked
            {
                availableItems = currentHead - currentTail;
            }
            int readCount = Math.Min(destination.Length, availableItems);

            if (readCount == 0)
                return 0;

            int tailIndex = currentTail & _mask;
            int rightLength = _capacity - tailIndex;

            if (readCount <= rightLength)
            {
                // 一次性顺序读取
                new Span<T>(_buffer, tailIndex, readCount).CopyTo(destination);
                ClearReferences(tailIndex, readCount);
            }
            else
            {
                // 绕回 (Wrap-around) 读取
                new Span<T>(_buffer, tailIndex, rightLength).CopyTo(destination.Slice(0, rightLength));
                new Span<T>(_buffer, 0, readCount - rightLength).CopyTo(destination.Slice(rightLength));

                ClearReferences(tailIndex, rightLength);
                ClearReferences(0, readCount - rightLength);
            }
            unchecked
            {
                Volatile.Write(ref _ptrs.Tail, currentTail + readCount);
            }
            return readCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ClearReferences(int startIndex, int length)
        {
            if (TypeTraits.NeedsClearing)
            {
                Array.Clear(_buffer, startIndex, length);
            }
        }

        /// <summary>
        /// 手动实现的 2的幂次方进位计算 (兼容不支持 BitOperations 的旧框架)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int RoundUpToPowerOf2(int value)
        {
            if (value < 2)
                return 2;
            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }

        public int Drain()
        {
            int currentTail = _ptrs.Tail;
            int currentHead = Volatile.Read(ref _ptrs.Head);

            int availableItems;
            unchecked
            {
                availableItems = currentHead - currentTail;
            }

            if (availableItems == 0)
                return 0;

            int tailIndex = currentTail & _mask;
            int rightLength = _capacity - tailIndex;

            if (availableItems <= rightLength)
            {
                ClearReferences(tailIndex, availableItems);
            }
            else
            {
                ClearReferences(tailIndex, rightLength);
                ClearReferences(0, availableItems - rightLength);
            }
            // 直接将 Tail 推到和 Head 对齐的位置，表示队列已空
            unchecked
            {
                Volatile.Write(ref _ptrs.Tail, currentTail + availableItems);
            }

            return availableItems;
        }

        /// <summary>
        /// 方案 B (危险，非线程安全)：强行重置整个队列的状态。
        /// ⚠️ 警告：调用此方法时，必须由外部业务逻辑保证：
        /// 此时【没有任何线程】正在调用 Enqueue 或 Dequeue（例如系统完全处于暂停或复位状态）。
        /// 否则将引发严重的并发灾难！
        /// </summary>
        public void UnsafeClear()
        {
            if (TypeTraits.NeedsClearing)
            {
                Array.Clear(_buffer, 0, _capacity);
            }

            // 强行重置双指针
            Volatile.Write(ref _ptrs.Head, 0);
            Volatile.Write(ref _ptrs.Tail, 0);
        }

        /// <summary>
        /// 内部帮助类：在类加载时确定 T 是否需要清理引用。
        /// 在现代运行时中会被 JIT 优化为绝对常量 (Constant Folding)。
        /// </summary>
        private static class TypeTraits
        {
            public static readonly bool NeedsClearing = CheckNeedsClearing();

            private static bool CheckNeedsClearing()
            {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
                // 现代框架：直接使用底层提供的内联判定
                return RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#else
                // .NET Framework 4.6.2 回退方案：加载时反射检查
                Type type = typeof(T);
                if (!type.IsValueType)
                    return true; // 是引用类型
                if (type.IsPrimitive || type.IsPointer || type.IsEnum)
                    return false; // 纯值类型

                return CheckStructForRef(type);
#endif
            }

#if !(NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
            private static bool CheckStructForRef(Type t)
            {
                var fields = t.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    if (!field.FieldType.IsValueType)
                        return true;
                    if (!field.FieldType.IsPrimitive && !field.FieldType.IsEnum)
                    {
                        if (CheckStructForRef(field.FieldType))
                            return true;
                    }
                }
                return false;
            }
#endif
        }
    }
}
