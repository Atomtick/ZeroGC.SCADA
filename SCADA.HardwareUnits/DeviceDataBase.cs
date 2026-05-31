using System;
using System.Collections.Generic;
using System.Text;

namespace SCADA.HardwareUnits
{
    public struct DeviceDataBase
    {
        public string Module { get; }
        public string Name { get; }
        public string Unit { get; }
    }
}
