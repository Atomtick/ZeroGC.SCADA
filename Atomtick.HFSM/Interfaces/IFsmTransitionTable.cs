using System;

namespace SCADA.HFSM.Interfaces
{
    public interface IFsmTransitionTable<TState, TCommand>
        where TState : Enum
        where TCommand : Enum
    {
        bool CanMatch(TCommand msgCmd, TState state);

        bool CanMatch(TCommand msgCmd, TState state, out Func<object[], bool> action, out TState nextState);

        void Register(TState currState, TState nextState, TCommand msgCmd, Func<object[], bool> action);

        void Register(TState currState, TState nextState, TCommand msgCmd);

        void Register(TState currState, IRoutine routine, TCommand msgCmd, TState relayState, TState nextState, TState abortState);
    }
}
