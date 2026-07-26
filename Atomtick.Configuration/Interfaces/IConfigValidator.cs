namespace Atomtick.Configuration.Interfaces
{
    public interface IConfigValidator
    {
        void ValidateValue(string config, string value);
        bool ValidateValue(string config, string value, out string errorMessage);
    }
}
