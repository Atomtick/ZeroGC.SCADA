using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SCADA.Data
{
    [StructLayout(LayoutKind.Auto)]
    public struct DataPoint
    {
        // 8 字节类型放在最前面
        public long Timestamp;
        public double? DoubleValue;
        // 引用类型 (64位系统下是指针，8字节)
        public string Name;
        public string StringValue;
        public DataType DataType;
    }
}
