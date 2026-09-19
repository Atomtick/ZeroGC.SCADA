using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.ObjectPool;

namespace Atomtick.UART.Best
{
    public enum ProcessProgress
    {
        Timeout,
        WaitProcess,
        Acked,
    }

    public interface IFilter
    {
        bool IsCorrespondingReply(ReadOnlySpan<byte> send, ReadOnlyMemory<byte> reply);

        bool TryFilter(ReadOnlySpan<byte> data, out int frameLength);
    }


    public class SerialPort 
    {
        #region Recv

        private readonly byte[] _buffer = new byte[512];
        private int _writePosition = 0;

        #endregion Recv

        private readonly System.IO.Ports.SerialPort _ports;

        public string Tag {  get; set; }
        private IFilter _filter;

        private Stopwatch _stopwatch = new Stopwatch();

        private Request256Bytes ActiveRequest;

        private ConcurrentQueue<Request256Bytes> controlRequestQueue;

        private List<Request256Bytes> controlResponseQueue;

        public event EventHandler<Request256Bytes> ErrorOcuured;

        private int index = 0;

        public SerialPort(IFilter filter, string portName, int baudrate, Parity parity, StopBits stopBits, int dataBits,string tag)
        {
            _ports = new System.IO.Ports.SerialPort(portName, baudrate, parity, dataBits);
            _filter = filter;
        }

        public event EventHandler<ReadOnlySpan<byte>> FrameReceived;

        public event EventHandler<(ReadOnlyMemory<byte>, byte[])> FrameReceivedWrap;

        public Channel<(ReadOnlyMemory<byte> frame, byte[] rentArray)> Channel { get; private set; }

        public ProcessProgress GetReuslt(Request256Bytes req)
        {
            return req.ProcessProgress;
        }

        public void Monitor() 
        {
            var activeReq = ActiveRequest;
            var idle = activeReq.Equals(Request256Bytes.Default);
            // 监控响应超时
            if (idle == false)
            {
                if (_stopwatch.ElapsedMilliseconds > activeReq.TimeoutMS)
                {
                    activeReq.ProcessProgress = ProcessProgress.Timeout;
                    ErrorOcuured?.Invoke(this, activeReq);
                }
            }

            // 让出信道
            if (activeReq.ProcessProgress == ProcessProgress.Acked)
            {
                ActiveRequest = default;
            }

            // 如果当前请求信道空闲，则发送新的请求
            if (ActiveRequest.Equals(Request256Bytes.Default) == false)
            {
                bool hasNewReq = true;
                Request256Bytes req;
                if (controlRequestQueue.TryDequeue(out req) == false)
                {
                    if (controlResponseQueue.Any())
                        req = controlResponseQueue[index++];
                    else
                        hasNewReq = false;
                }
                if (hasNewReq)
                {
                    _stopwatch.Restart();
                    _ports.BaseStream.Write(req.Data.Slice(0, req.Length));
                }
            }

            // 如果响应队列有元素，则处理响应
            if (Channel.Reader.TryRead(out var item))
            {
                try
                {
                    FrameReceived?.Invoke(this, item.frame.Span);
                    if (_filter.IsCorrespondingReply(ActiveRequest.Data, item.frame))
                    {
                        // 释放发送信道
                        ActiveRequest = default;
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(item.rentArray);
                }
            }
        }

        public Request256Bytes Request(Request256Bytes req)
        {
            controlRequestQueue.Enqueue(req);
            return req;
        }

        internal void ReadHardwareBuffer()
        {
            if (_ports.IsOpen)
            {
                if (_ports.BytesToRead > 0)
                {
                    Span<byte> writeSpan = _buffer.AsSpan(_writePosition);
                    int bytesRead = _ports.BaseStream.Read(writeSpan);
                    if (bytesRead == 0)
                        return;

                    int readPosition = 0;
                    Span<byte> processSpan = _buffer.AsSpan(0, _writePosition);

                    while (_filter.TryFilter(processSpan.Slice(readPosition), out int frameLength))
                    {
                        // 抛出完整帧的事件
                        ReadOnlySpan<byte> frame = processSpan.Slice(readPosition, frameLength);
                        // 1. ArrayPool 借出的是裸数组 byte[]，完全复用，绝对 0 分配
                        byte[] rentedArray = ArrayPool<byte>.Shared.Rent(frame.Length);

                        // 2. 将数据拷贝到借出的数组中
                        frame.CopyTo(rentedArray);

                        // 3. Memory<T> 是一个只分配在栈上的 struct！将数组包装为 Memory，0 分配
                        ReadOnlyMemory<byte> safeFrame = new ReadOnlyMemory<byte>(rentedArray, 0, frame.Length);

                        Channel.Writer.TryWrite((safeFrame, rentedArray));

                        // 游标向前推进
                        readPosition += frameLength;
                    }

                    // 3. 滑动窗口：把剩下的“半包”挪到数组的最前面
                    int remaining = _writePosition - readPosition;
                    if (remaining > 0 && readPosition > 0)
                    {
                        // 极速内存搬运，底层处理了重叠，绝对零 GC
                        Span<byte> remainingSpan = _buffer.AsSpan(readPosition, remaining);
                        remainingSpan.CopyTo(_buffer.AsSpan(0));
                    }

                    // 更新写入指针到剩下的数据尾部，等待下一轮读取
                    _writePosition = remaining;

                    //_writePosition += bytesRead;

                    //_ports.BaseStream.Read()

                    //int count = _ports.Read(bytes, 0, _ports.BytesToRead);
                    //var spannn = bytes.AsSpan<byte>(count);
                    //if (_filter.Filter(spannn))
                    //{
                    //    ReadOnlySpan<byte> spp = stackalloc byte[100];
                    //    FrameReceived?.Invoke(this, spp);
                    //}
                }
            }
        }
    }
}