namespace SCADA.ObjectModel
{
    public interface IDeviceManager
    {
        void AddDevice(IDevice device);

        IDevice GetDeviceById(string deviceId);

        void RemoveDevice(IDevice device);
    }
}