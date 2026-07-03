namespace SCADA.Common
{
    public interface IConnection
    {
        string Address { get; set; }

        void Connect();

        void Disconnect();

        bool IsConnected();
    }
}