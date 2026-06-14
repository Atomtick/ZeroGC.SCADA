using System;
using System.Collections.Generic;
using SCADA.Configuration;

namespace SCADA.Configuration.Interfaces
{
    public interface IConfigReader
    {
        ConfigItem SelectConfigItem(string config);
        ConfigValue Read(ConfigItem configItem);
        (ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2);
        (ConfigValue, ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2, ConfigItem configItem3);
    }
}
