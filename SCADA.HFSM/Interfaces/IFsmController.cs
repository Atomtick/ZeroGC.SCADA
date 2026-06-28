using System;

namespace SCADA.HFSM.Interfaces
{
    public interface IFsmController : IFsmMessenger
    {
        Enum CurrState { get; }
        Enum PrevState { get; }
        void Start();
        void Stop();
        void AdjustInterval(int interval);
        void RestoreDefaultInterval();
    }
}
