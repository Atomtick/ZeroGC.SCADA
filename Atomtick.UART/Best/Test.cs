using System;
using System.Collections.Generic;
using System.Text;

namespace Atomtick.UART.Best
{
    internal class Test
    {
        public Test()
        {
            Request256Bytes send = new Request256Bytes();
            send.Data.Fill((byte)'H');
            send.Data.Fill((byte)'C');
            send.Data.Fill((byte)'S');
            send.Data.Fill(1);
            send.Length = send.Data.Length;
        }
    }
}
