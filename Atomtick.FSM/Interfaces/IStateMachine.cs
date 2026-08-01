namespace Atomtick.FSM.Interfaces
{
    public interface IStateMachine : IFsmController, IFsmTransitionTable
    {
        public string Name { get; }
    }
}
