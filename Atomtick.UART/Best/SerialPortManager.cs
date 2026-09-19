using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Atomtick.UART.Best
{
    public class SerialPortManager
    {
        public static SerialPortManager Instance { get; } = new SerialPortManager();

        private readonly IDictionary<string, SerialPort>  _ports;

        private SerialPortManager()
        {
            _ports = new ConcurrentDictionary<string, SerialPort>(); 
        }

        public SerialPort Create(IFilter filter, string portName, int baudrate, Parity parity, StopBits stopBits, int dataBits, string tag)
        {
            var result = new SerialPort(filter, portName, baudrate, parity, stopBits, dataBits, tag);
            if (_ports.TryAdd(portName, result) == false)
            {
                throw new ArgumentException();
            }
            return result;
        }

        public void Initialize()
        {
            new Thread(new ThreadStart(task)).Start();
        }

        private void task()
        {
            foreach (var port in _ports.Values)
            {
                port.Monitor();
            }
        }
    }
}
