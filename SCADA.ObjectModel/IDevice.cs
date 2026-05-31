namespace SCADA.ObjectModel
{
    public interface IDevice
    {
        void Initialize();

        void Monitor();

        void Reset();

        void Terminate();

        void Update();
    }
}