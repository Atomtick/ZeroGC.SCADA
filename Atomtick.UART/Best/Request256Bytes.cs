using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Atomtick.UART.Best
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Request256Bytes : IEquatable<Request256Bytes>
    {
        const int MAX_REQ_FRAME_LEN = 256;

        public long Id { get; }
        private fixed byte _data[256];
        public int Length { get; set; }
        public int TimeoutMS { get; set; }
        public int RetryCount { get; set; }
    
        public ProcessProgress ProcessProgress { get; set; }

        public bool IsAck { get; set;  }

        public static Request256Bytes Default = new Request256Bytes();

        public Span<byte> Data
        {
            get
            {
                //fixed (byte* ptr = Payload)
                //{
                //    return new Span<byte>(ptr, 256);
                //}
                return MemoryMarshal.CreateSpan(ref _data[0], MAX_REQ_FRAME_LEN);
            }
        }

        public bool Equals(Request256Bytes other)
        {
            throw new NotImplementedException();
        }
    }
}
