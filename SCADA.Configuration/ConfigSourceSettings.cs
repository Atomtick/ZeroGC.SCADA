using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Configuration
{
    public class ConfigSourceSettings
    {
        public bool SupportAtomicOperations { get; set; }
        public bool TrackConfigValueModification { get; set; }
        public bool RestoreOnAppStartup { get; set; }
        public CustomizeOptions CustomizeOptions { get; set; }
        public AppendValidationRule AdditionalValidationRule { get; set; }
    }
}
