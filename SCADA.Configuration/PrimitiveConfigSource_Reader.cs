using SCADA.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCADA.Configuration
{
    // 对用户应用暴露的接口,提供原子性的读取多个配置项的值的能力.但是不暴露修改配置项值的能力
    public partial class PrimitiveConfigSource : IConfigSourceReader
    {
        public void Read(IConfigItem configItem, out IConfigValue value)
        {
            int version;
            do
            {
                var item = configItem as ConfigItem;
                version = _seqLock.ReadBegin();
                value = new ConfigValue(item.ObjectValue, item.StringValue, item.Name, item.Type);
            } while (_seqLock.ReadRetry(version));
        }

        public void Read(IConfigItem configItem1, out IConfigValue value1, IConfigItem configItem2, out IConfigValue value2)
        {
            int version;
            do
            {
                var item1 = configItem1 as ConfigItem;
                var item2 = configItem2 as ConfigItem;
                version = _seqLock.ReadBegin();
                value1 = new ConfigValue(item1.ObjectValue, item1.StringValue, item1.Name, item1.Type);
                value2 = new ConfigValue(item2.ObjectValue, item2.StringValue, item2.Name, item2.Type);
            } while (_seqLock.ReadRetry(version));
        }

        public void Read(IConfigItem configItem1, out IConfigValue value1, IConfigItem configItem2, out IConfigValue value2, IConfigItem configItem3, out IConfigValue value3)
        {
            int version;
            do
            {
                var item1 = configItem1 as ConfigItem;
                var item2 = configItem2 as ConfigItem;
                var item3 = configItem3 as ConfigItem;
                version = _seqLock.ReadBegin();
                value1 = new ConfigValue(item1.ObjectValue, item1.StringValue, item1.Name, item1.Type);
                value2 = new ConfigValue(item2.ObjectValue, item2.StringValue, item2.Name, item2.Type);
                value3 = new ConfigValue(item3.ObjectValue, item3.StringValue, item3.Name, item3.Type);
            } while (_seqLock.ReadRetry(version));
        }

        public IConfigItem SelectConfigItem(string config)
        {
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentNullException(nameof(config));
            }
            if (!_configItems.TryGetValue(config, out var result))
            {
                result = new ConfigItem()
                {
                    Name = config,
                    ObjectValue = null,
                    StringValue = null,
                    Type = 0
                };
            }
            return result;
        }
    }
}