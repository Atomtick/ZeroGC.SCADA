using System;

namespace Atomtick.FSM
{
    internal class TransitionCondition
    {
        public Enum MsgCmd { get; set; }
        public Func<object[], bool> Action { get; set; }
        public Enum NextState { get; set; }
    }
}
