namespace SCADA.Common.Interfaces
{
    public interface IConfigSourceWriter
    {
        IConfigSourceWriter BeginTransaction(out long transactionId);
        void CommitTransaction(long transactionId);
        IConfigSourceWriter Set(long transactionId, string config, object value);
    }
}