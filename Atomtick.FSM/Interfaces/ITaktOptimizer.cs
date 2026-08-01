using System;
using System.Collections.Generic;
using System.Text;

namespace Atomtick.FSM.Interfaces
{
    public interface ITaktOptimizer
    {
        ITaktOptimizer FineTuneCheckInterval(TimeSpan timeSpan);
    }
}
