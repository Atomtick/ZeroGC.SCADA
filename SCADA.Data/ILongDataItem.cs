namespace SCADA.Data
{
    public interface ILongDataItem
    {
        long? Get(out long timestamp);

        void Update(long? value, long timestamp);
    }
}