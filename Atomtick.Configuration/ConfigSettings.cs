namespace Atomtick.Configuration
{
    public class ConfigSettings
    {
        public bool IsConfigModificationTrackingEnabled { get; set; } = true;
        public bool RestoreOnAppStartup { get; set; } = false;
        public CustomizeOptions CustomizeOptions { get; set; }
        public AppendValidationRule AppendedValidationRule { get; set; }
    }
}