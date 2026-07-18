namespace Atomtick.Configuration.Interfaces
{
    public interface IConfigValidator
    {
        void ValidateValue(string config, object value);
        bool ValidateValue(string config, object value, out string errorMessage);
        void ValidateValue(string config, string value);
        bool ValidateValue(string config, string value, out string errorMessage);
    }
}
