using System;
using SCADA.Configuration.Interfaces;

namespace SCADA.Configuration
{
    // 对用户应用暴露的接口,提供原子性的读取多个配置项的值的能力.但是不暴露修改配置项值的能力
    public partial class PrimitiveConfigSource : IConfigReader
    {
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

        public ConfigValue Read(ConfigItem configItem)
        {
            ConfigValue v;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v = new ConfigValue(configItem.ObjectValue, configItem.StringValue, configItem.Path, configItem.Type);
            } while (_seqLock.ReadRetry(version));
            return v;
        }

        public (ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2)
        {
            ConfigValue v1,
                v2;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2);
        }

        public (ConfigValue, ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2, ConfigItem configItem3)
        {
            ConfigValue v1,
                v2,
                v3;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(ConfigItem configItem1, ConfigItem configItem2, ConfigItem configItem3, ConfigItem configItem4)
        {
            ConfigValue v1,
                v2,
                v3,
                v4;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
        }

        public (ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue, ConfigValue) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
            } while (_seqLock.ReadRetry(version));
            return (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37);
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58,
            ConfigItem configItem59
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
                v59 = new ConfigValue(configItem59.ObjectValue, configItem59.StringValue, configItem59.Path, configItem59.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58,
            ConfigItem configItem59,
            ConfigItem configItem60
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
                v59 = new ConfigValue(configItem59.ObjectValue, configItem59.StringValue, configItem59.Path, configItem59.Type);
                v60 = new ConfigValue(configItem60.ObjectValue, configItem60.StringValue, configItem60.Path, configItem60.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58,
            ConfigItem configItem59,
            ConfigItem configItem60,
            ConfigItem configItem61
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
                v59 = new ConfigValue(configItem59.ObjectValue, configItem59.StringValue, configItem59.Path, configItem59.Type);
                v60 = new ConfigValue(configItem60.ObjectValue, configItem60.StringValue, configItem60.Path, configItem60.Type);
                v61 = new ConfigValue(configItem61.ObjectValue, configItem61.StringValue, configItem61.Path, configItem61.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58,
            ConfigItem configItem59,
            ConfigItem configItem60,
            ConfigItem configItem61,
            ConfigItem configItem62
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61,
                v62;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
                v59 = new ConfigValue(configItem59.ObjectValue, configItem59.StringValue, configItem59.Path, configItem59.Type);
                v60 = new ConfigValue(configItem60.ObjectValue, configItem60.StringValue, configItem60.Path, configItem60.Type);
                v61 = new ConfigValue(configItem61.ObjectValue, configItem61.StringValue, configItem61.Path, configItem61.Type);
                v62 = new ConfigValue(configItem62.ObjectValue, configItem62.StringValue, configItem62.Path, configItem62.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61,
                v62
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58,
            ConfigItem configItem59,
            ConfigItem configItem60,
            ConfigItem configItem61,
            ConfigItem configItem62,
            ConfigItem configItem63
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61,
                v62,
                v63;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
                v59 = new ConfigValue(configItem59.ObjectValue, configItem59.StringValue, configItem59.Path, configItem59.Type);
                v60 = new ConfigValue(configItem60.ObjectValue, configItem60.StringValue, configItem60.Path, configItem60.Type);
                v61 = new ConfigValue(configItem61.ObjectValue, configItem61.StringValue, configItem61.Path, configItem61.Type);
                v62 = new ConfigValue(configItem62.ObjectValue, configItem62.StringValue, configItem62.Path, configItem62.Type);
                v63 = new ConfigValue(configItem63.ObjectValue, configItem63.StringValue, configItem63.Path, configItem63.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61,
                v62,
                v63
            );
        }

        public (
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue,
            ConfigValue
        ) Read(
            ConfigItem configItem1,
            ConfigItem configItem2,
            ConfigItem configItem3,
            ConfigItem configItem4,
            ConfigItem configItem5,
            ConfigItem configItem6,
            ConfigItem configItem7,
            ConfigItem configItem8,
            ConfigItem configItem9,
            ConfigItem configItem10,
            ConfigItem configItem11,
            ConfigItem configItem12,
            ConfigItem configItem13,
            ConfigItem configItem14,
            ConfigItem configItem15,
            ConfigItem configItem16,
            ConfigItem configItem17,
            ConfigItem configItem18,
            ConfigItem configItem19,
            ConfigItem configItem20,
            ConfigItem configItem21,
            ConfigItem configItem22,
            ConfigItem configItem23,
            ConfigItem configItem24,
            ConfigItem configItem25,
            ConfigItem configItem26,
            ConfigItem configItem27,
            ConfigItem configItem28,
            ConfigItem configItem29,
            ConfigItem configItem30,
            ConfigItem configItem31,
            ConfigItem configItem32,
            ConfigItem configItem33,
            ConfigItem configItem34,
            ConfigItem configItem35,
            ConfigItem configItem36,
            ConfigItem configItem37,
            ConfigItem configItem38,
            ConfigItem configItem39,
            ConfigItem configItem40,
            ConfigItem configItem41,
            ConfigItem configItem42,
            ConfigItem configItem43,
            ConfigItem configItem44,
            ConfigItem configItem45,
            ConfigItem configItem46,
            ConfigItem configItem47,
            ConfigItem configItem48,
            ConfigItem configItem49,
            ConfigItem configItem50,
            ConfigItem configItem51,
            ConfigItem configItem52,
            ConfigItem configItem53,
            ConfigItem configItem54,
            ConfigItem configItem55,
            ConfigItem configItem56,
            ConfigItem configItem57,
            ConfigItem configItem58,
            ConfigItem configItem59,
            ConfigItem configItem60,
            ConfigItem configItem61,
            ConfigItem configItem62,
            ConfigItem configItem63,
            ConfigItem configItem64
        )
        {
            ConfigValue v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61,
                v62,
                v63,
                v64;
            int version;
            do
            {
                version = _seqLock.ReadBegin();
                v1 = new ConfigValue(configItem1.ObjectValue, configItem1.StringValue, configItem1.Path, configItem1.Type);
                v2 = new ConfigValue(configItem2.ObjectValue, configItem2.StringValue, configItem2.Path, configItem2.Type);
                v3 = new ConfigValue(configItem3.ObjectValue, configItem3.StringValue, configItem3.Path, configItem3.Type);
                v4 = new ConfigValue(configItem4.ObjectValue, configItem4.StringValue, configItem4.Path, configItem4.Type);
                v5 = new ConfigValue(configItem5.ObjectValue, configItem5.StringValue, configItem5.Path, configItem5.Type);
                v6 = new ConfigValue(configItem6.ObjectValue, configItem6.StringValue, configItem6.Path, configItem6.Type);
                v7 = new ConfigValue(configItem7.ObjectValue, configItem7.StringValue, configItem7.Path, configItem7.Type);
                v8 = new ConfigValue(configItem8.ObjectValue, configItem8.StringValue, configItem8.Path, configItem8.Type);
                v9 = new ConfigValue(configItem9.ObjectValue, configItem9.StringValue, configItem9.Path, configItem9.Type);
                v10 = new ConfigValue(configItem10.ObjectValue, configItem10.StringValue, configItem10.Path, configItem10.Type);
                v11 = new ConfigValue(configItem11.ObjectValue, configItem11.StringValue, configItem11.Path, configItem11.Type);
                v12 = new ConfigValue(configItem12.ObjectValue, configItem12.StringValue, configItem12.Path, configItem12.Type);
                v13 = new ConfigValue(configItem13.ObjectValue, configItem13.StringValue, configItem13.Path, configItem13.Type);
                v14 = new ConfigValue(configItem14.ObjectValue, configItem14.StringValue, configItem14.Path, configItem14.Type);
                v15 = new ConfigValue(configItem15.ObjectValue, configItem15.StringValue, configItem15.Path, configItem15.Type);
                v16 = new ConfigValue(configItem16.ObjectValue, configItem16.StringValue, configItem16.Path, configItem16.Type);
                v17 = new ConfigValue(configItem17.ObjectValue, configItem17.StringValue, configItem17.Path, configItem17.Type);
                v18 = new ConfigValue(configItem18.ObjectValue, configItem18.StringValue, configItem18.Path, configItem18.Type);
                v19 = new ConfigValue(configItem19.ObjectValue, configItem19.StringValue, configItem19.Path, configItem19.Type);
                v20 = new ConfigValue(configItem20.ObjectValue, configItem20.StringValue, configItem20.Path, configItem20.Type);
                v21 = new ConfigValue(configItem21.ObjectValue, configItem21.StringValue, configItem21.Path, configItem21.Type);
                v22 = new ConfigValue(configItem22.ObjectValue, configItem22.StringValue, configItem22.Path, configItem22.Type);
                v23 = new ConfigValue(configItem23.ObjectValue, configItem23.StringValue, configItem23.Path, configItem23.Type);
                v24 = new ConfigValue(configItem24.ObjectValue, configItem24.StringValue, configItem24.Path, configItem24.Type);
                v25 = new ConfigValue(configItem25.ObjectValue, configItem25.StringValue, configItem25.Path, configItem25.Type);
                v26 = new ConfigValue(configItem26.ObjectValue, configItem26.StringValue, configItem26.Path, configItem26.Type);
                v27 = new ConfigValue(configItem27.ObjectValue, configItem27.StringValue, configItem27.Path, configItem27.Type);
                v28 = new ConfigValue(configItem28.ObjectValue, configItem28.StringValue, configItem28.Path, configItem28.Type);
                v29 = new ConfigValue(configItem29.ObjectValue, configItem29.StringValue, configItem29.Path, configItem29.Type);
                v30 = new ConfigValue(configItem30.ObjectValue, configItem30.StringValue, configItem30.Path, configItem30.Type);
                v31 = new ConfigValue(configItem31.ObjectValue, configItem31.StringValue, configItem31.Path, configItem31.Type);
                v32 = new ConfigValue(configItem32.ObjectValue, configItem32.StringValue, configItem32.Path, configItem32.Type);
                v33 = new ConfigValue(configItem33.ObjectValue, configItem33.StringValue, configItem33.Path, configItem33.Type);
                v34 = new ConfigValue(configItem34.ObjectValue, configItem34.StringValue, configItem34.Path, configItem34.Type);
                v35 = new ConfigValue(configItem35.ObjectValue, configItem35.StringValue, configItem35.Path, configItem35.Type);
                v36 = new ConfigValue(configItem36.ObjectValue, configItem36.StringValue, configItem36.Path, configItem36.Type);
                v37 = new ConfigValue(configItem37.ObjectValue, configItem37.StringValue, configItem37.Path, configItem37.Type);
                v38 = new ConfigValue(configItem38.ObjectValue, configItem38.StringValue, configItem38.Path, configItem38.Type);
                v39 = new ConfigValue(configItem39.ObjectValue, configItem39.StringValue, configItem39.Path, configItem39.Type);
                v40 = new ConfigValue(configItem40.ObjectValue, configItem40.StringValue, configItem40.Path, configItem40.Type);
                v41 = new ConfigValue(configItem41.ObjectValue, configItem41.StringValue, configItem41.Path, configItem41.Type);
                v42 = new ConfigValue(configItem42.ObjectValue, configItem42.StringValue, configItem42.Path, configItem42.Type);
                v43 = new ConfigValue(configItem43.ObjectValue, configItem43.StringValue, configItem43.Path, configItem43.Type);
                v44 = new ConfigValue(configItem44.ObjectValue, configItem44.StringValue, configItem44.Path, configItem44.Type);
                v45 = new ConfigValue(configItem45.ObjectValue, configItem45.StringValue, configItem45.Path, configItem45.Type);
                v46 = new ConfigValue(configItem46.ObjectValue, configItem46.StringValue, configItem46.Path, configItem46.Type);
                v47 = new ConfigValue(configItem47.ObjectValue, configItem47.StringValue, configItem47.Path, configItem47.Type);
                v48 = new ConfigValue(configItem48.ObjectValue, configItem48.StringValue, configItem48.Path, configItem48.Type);
                v49 = new ConfigValue(configItem49.ObjectValue, configItem49.StringValue, configItem49.Path, configItem49.Type);
                v50 = new ConfigValue(configItem50.ObjectValue, configItem50.StringValue, configItem50.Path, configItem50.Type);
                v51 = new ConfigValue(configItem51.ObjectValue, configItem51.StringValue, configItem51.Path, configItem51.Type);
                v52 = new ConfigValue(configItem52.ObjectValue, configItem52.StringValue, configItem52.Path, configItem52.Type);
                v53 = new ConfigValue(configItem53.ObjectValue, configItem53.StringValue, configItem53.Path, configItem53.Type);
                v54 = new ConfigValue(configItem54.ObjectValue, configItem54.StringValue, configItem54.Path, configItem54.Type);
                v55 = new ConfigValue(configItem55.ObjectValue, configItem55.StringValue, configItem55.Path, configItem55.Type);
                v56 = new ConfigValue(configItem56.ObjectValue, configItem56.StringValue, configItem56.Path, configItem56.Type);
                v57 = new ConfigValue(configItem57.ObjectValue, configItem57.StringValue, configItem57.Path, configItem57.Type);
                v58 = new ConfigValue(configItem58.ObjectValue, configItem58.StringValue, configItem58.Path, configItem58.Type);
                v59 = new ConfigValue(configItem59.ObjectValue, configItem59.StringValue, configItem59.Path, configItem59.Type);
                v60 = new ConfigValue(configItem60.ObjectValue, configItem60.StringValue, configItem60.Path, configItem60.Type);
                v61 = new ConfigValue(configItem61.ObjectValue, configItem61.StringValue, configItem61.Path, configItem61.Type);
                v62 = new ConfigValue(configItem62.ObjectValue, configItem62.StringValue, configItem62.Path, configItem62.Type);
                v63 = new ConfigValue(configItem63.ObjectValue, configItem63.StringValue, configItem63.Path, configItem63.Type);
                v64 = new ConfigValue(configItem64.ObjectValue, configItem64.StringValue, configItem64.Path, configItem64.Type);
            } while (_seqLock.ReadRetry(version));
            return (
                v1,
                v2,
                v3,
                v4,
                v5,
                v6,
                v7,
                v8,
                v9,
                v10,
                v11,
                v12,
                v13,
                v14,
                v15,
                v16,
                v17,
                v18,
                v19,
                v20,
                v21,
                v22,
                v23,
                v24,
                v25,
                v26,
                v27,
                v28,
                v29,
                v30,
                v31,
                v32,
                v33,
                v34,
                v35,
                v36,
                v37,
                v38,
                v39,
                v40,
                v41,
                v42,
                v43,
                v44,
                v45,
                v46,
                v47,
                v48,
                v49,
                v50,
                v51,
                v52,
                v53,
                v54,
                v55,
                v56,
                v57,
                v58,
                v59,
                v60,
                v61,
                v62,
                v63,
                v64
            );
        }
    }
}
