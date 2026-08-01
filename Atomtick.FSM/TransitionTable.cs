using System;
using System.Collections.Generic;

namespace Atomtick.FSM
{
    internal class TransitionTable : Dictionary<Enum, TransitionConditions> { }
}
