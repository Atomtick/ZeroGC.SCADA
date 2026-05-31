namespace SCADA.Data
{
    public interface IStringDataItem
    {
        string Get(out long timestamp);

        void Update(string value, long timestamp);
    }
}