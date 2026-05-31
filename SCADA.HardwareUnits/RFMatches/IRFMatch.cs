using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.HardwareUnits.RFMatches
{
    public readonly struct DeviceData
    {
        public DeviceDataBase BaseData { get; }
        
        public WorkMode WorkModeSetpoint { get; }
        public WorkMode WorkModeFeedback { get; }

    }


    public interface IRFMatch
    {

    }
}