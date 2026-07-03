using SCADA.Common;
using SCADA.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.PLCFramework
{
    public interface IPlcAdapter : IConnection, IDisposable
    {
        #region Read & Write
        ReadOnlySpan<byte> ReadAIs(string startAddr, int length);
        ReadOnlySpan<byte> ReadAOs(string startAddr, int length);
        ReadOnlySpan<bool> ReadDIs(string startAddr, int length);
        ReadOnlySpan<bool> ReadDOs(string startAddr, int length);
        bool WriteAOs(string startAddr, IList<byte> values);
        bool WriteDOs(string startAddr, IList<bool> values);
        #endregion Read & Write

        public SCADA.Common.ByteOrder2 Int16ByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder2 UInt16ByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder4 Int32ByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder4 UInt32ByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder8 Int64ByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder8 UInt64ByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder4 FloatByteOrder { get; protected set; }
        public SCADA.Common.ByteOrder8 DoubleByteOrder { get; protected set; }
    }
}