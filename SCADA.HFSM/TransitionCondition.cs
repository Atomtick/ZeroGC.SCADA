using System;

namespace SCADA.HFSM
{
    internal class TransitionCondition
    {
        public Enum MsgCmd { get; set; }
        public Func<object[], bool> Action { get; set; }
        public Enum NextState { get; set; }
    }
}
