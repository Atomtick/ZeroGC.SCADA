using System;
using System.Collections.Generic;
using System.Text;

namespace Atomtick.UART.Best
{
    public class SerialPort
    {
        public EventHandler<byte[]> DataReceived;

        public int Send(byte[] data)
        {
            return 0;
        }
    }
}
