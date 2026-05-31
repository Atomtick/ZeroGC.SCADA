namespace SCADA.Data
{
    public interface IObjectDataItem
    {
        object Get(out long timestamp);

        void Update(object value, long timestamp);
    }
}