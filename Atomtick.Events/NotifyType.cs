using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Events
{
    [Flags]
    public enum NotifyType
    {
        UILog = 1,
        Dialog = 4,
        Sound = 8,
        KickOut = 16,
        Toast = 32,
    }
}
