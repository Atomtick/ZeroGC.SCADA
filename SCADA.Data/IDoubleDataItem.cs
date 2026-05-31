namespace SCADA.Data
{
    public interface IDoubleDataItem
    {
        double? Get(out long timestamp);

        void Update(double? value, long timestamp);
    }
}