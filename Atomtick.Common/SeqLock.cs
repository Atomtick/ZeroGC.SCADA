using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCADA.Common
{
    /// <summary>
    /// 生产级 SeqLock 实现 (极致性能优化版)
    /// 使用 128 字节结构体和 64 字节偏移量，实现前后双向物理隔离。
    /// 彻底免疫任何相邻变量修改引发的 CPU 缓存伪共享 (False Sharing) 问题。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct SeqLock
    {
        // 核心序列号。
        // 放置在偏移量 64 的位置，意味着它前面有 64 字节空白，后面有 60 字节空白。
        // 无论这个结构体被分配在内存的哪个边界，_sequence 绝对会独占一个 Cache Line。
        [FieldOffset(64)]
        private int _sequence;

        /// <summary>
        /// 读操作：开始读取，获取当前稳定版本号
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReadBegin()
        {
            var spin = new SpinWait();
            while (true)
            {
                // Volatile.Read 保证每次都从主存/最新缓存取值，而非寄存器旧值
                int seq = Volatile.Read(ref _sequence);

                // 核心逻辑：序列号必须是偶数才能读。奇数表示写线程正在修改数据。
                if ((seq & 1) == 0)
                {
                    // 读内存屏障：防止 CPU 将下方读取受保护数据的指令，重排序到读取版本号之前
                    // Thread.MemoryBarrier();
                    return seq;
                }

                // 遇到写线程正在操作，执行轻量级自旋等待
                spin.SpinOnce();
            }
        }

        /// <summary>
        /// 读操作：校验读取期间数据是否被污染
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadRetry(int startSequence)
        {
            // 读内存屏障：防止 CPU 将上方读取受保护数据的指令，重排序到第二次读取版本号之后
            Thread.MemoryBarrier();

            // 如果现在的版本号不等于开始时的版本号，说明中间发生了写入，需要重试
            return startSequence != Volatile.Read(ref _sequence);
        }

        /// <summary>
        /// 写操作：获取排他锁 (支持多写者安全竞争)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLock()
        {
            var spin = new SpinWait();
            while (true)
            {
                int seq = Volatile.Read(ref _sequence);

                // 必须是偶数，且当前线程成功通过 CAS 将其变为奇数，才算拿到锁
                if ((seq & 1) == 0 && Interlocked.CompareExchange(ref _sequence, seq + 1, seq) == seq)
                {
                    // 写内存屏障：确保后续更新实际数据的操作，绝对不会跑到获取锁之前执行
                    // Thread.MemoryBarrier();
                    break;
                }
                spin.SpinOnce();
            }
        }

        /// <summary>
        /// 写操作：释放锁
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUnlock()
        {
            // 写内存屏障：确保前面更新受保护数据的指令已经彻底落盘生效
            // Thread.MemoryBarrier();

            // 奇数加 1 变回偶数，表示写入完成，其他读/写线程可以继续
            Interlocked.Increment(ref _sequence);
        }
    }
}
