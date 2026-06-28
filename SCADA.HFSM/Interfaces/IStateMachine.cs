namespace SCADA.HFSM.Interfaces
{
    public interface IStateMachine : IFsmController, IFsmTransitionTable
    {
        public string Name { get; }
    }
}
