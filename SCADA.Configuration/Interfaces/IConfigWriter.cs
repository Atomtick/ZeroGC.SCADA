namespace SCADA.Configuration.Interfaces
{
    public interface IConfigWriter
    {
        IConfigWriter BeginTransaction(out long transactionId);
        void CommitTransaction(long transactionId);
        IConfigWriter Write(long transactionId, string config, object value);
    }
}
