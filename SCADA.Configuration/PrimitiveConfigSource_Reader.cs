using System;
using SCADA.Configuration.Interfaces;

namespace SCADA.Configuration
{
    // 对用户应用暴露的接口,提供原子性的读取多个配置项的值的能力.但是不暴露修改配置项值的能力
    public partial class PrimitiveConfigSource : IConfigReader
    {
        public ConfigValue Read(ConfigItem configItem)
        {
            int version;
            do
            {
                ConfigItem item = configItem;
                version = _seqLock.ReadBegin();
                return new ConfigValue(item.ObjectValue, item.StringValue, item.Path, item.Type);
            } while (_seqLock.ReadRetry(version));
        }

        public (ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2)
        {
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                return (
                    new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type),
                    new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type)
                );
            } while (_seqLock.ReadRetry(version));
        }

        public (ConfigValue, ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2, ConfigItem configItem3)
        {
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                return (
                    new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type),
                    new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type),
                    new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type)
                );
            } while (_seqLock.ReadRetry(version));
        }

        public ConfigItem SelectConfigItem(string config)
        {
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentNullException(nameof(config));
            }
            if (!_configItems.TryGetValue(config, out ConfigItem result))
            {
                result = new ConfigItem()
                {
                    Name = string.Empty,
                    Path = config,
                    ObjectValue = null,
                    StringValue = null,
                    Type = ConfigType.Unknown,
                };
            }
            return result;
        }
    }
}
