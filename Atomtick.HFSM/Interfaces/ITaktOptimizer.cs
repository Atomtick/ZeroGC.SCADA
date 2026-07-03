using System;
using System.Collections.Generic;
using System.Text;

namespace SCADA.HFSM.Interfaces
{
    public interface ITaktOptimizer
    {
        ITaktOptimizer FineTuneCheckInterval(TimeSpan timeSpan);
    }
}
