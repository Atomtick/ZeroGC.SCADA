namespace SCADA.Data
{
    public interface IBoolDataItem
    {
        bool? Get(out long timestamp);

        void Update(bool? value, long timestamp);
    }
}