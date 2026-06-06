namespace SCADA.Common.Interfaces
{
    public interface IConfigWriter
    {
        IConfigWriter BeginTransaction(out long transactionId);
        void CommitTransaction(long transactionId);
        IConfigWriter Set(long transactionId, string config, object value);
    }
}