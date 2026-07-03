using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCADA.Common;

namespace SCADA.PLCFramework.Test
{
    internal class Program
    {
        static void Main(string[] args) { }
    }
}

class Test
{
    private Dictionary<string, uint> _dict = new Dictionary<string, uint>();

    public Test()
    {
        _dict.Add(nameof(SetTemp), 0);
    }

    // 相当于单通道的串口通信. 因为PLC交互地址只有一份.
    public void SetTemp(float temp)
    {
        var transcationId = Interlocked.Increment(ref _dict[nameof(SetTemp)]);

        var a = 1;
        var b = 2;
        var c = a + b;
    }
}

class PLC
{
    void Read();

    void Write();
}

class InactiveHandler
{
    private uint _cmdId = 0;
    private ushort _cmdTransactionId = 0;
    private ushort echoTransactionId = 0;
    private byte _errorCode = 0;
    SpScRingBuffer<int> _spScRingBuffer = new SpScRingBuffer<int>(100);

    public InactiveHandler(PLC plc) { }

    void Initialize() { }

    void Monitor() { }

    void Reset()
    {
        _spScRingBuffer.UnsafeClear();
    }

    ushort Action<T>(string reqParam)
    {
        return (ushort)(Interlocked.Increment(ref _cmdId) % ushort.MaxValue);
    }

    byte IsCompleted(uint transactionId, string echoId)
    {
        if (int.Parse(echoId) == _cmdTransactionId)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
