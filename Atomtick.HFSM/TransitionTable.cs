using System;
using System.Collections.Generic;

namespace SCADA.HFSM
{
    internal class TransitionTable : Dictionary<Enum, TransitionConditions> { }
}
