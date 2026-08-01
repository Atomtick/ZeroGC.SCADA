using System;

namespace Atomtick.FSM.Interfaces
{
    public interface IFsmMessenger<TState, TMsg>
        where TState : Enum
        where TMsg : Enum
    {
        (bool isSuccess, TState currentState) PostMsg(TMsg msgCmd, params object[] args);
        void ClearMsgQueue();
    }
}
