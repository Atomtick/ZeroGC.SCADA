using SCADA.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource : IConfigSourceWriter
    {
        // long 事务id,允许多个事务并行.
        // ConcurrentDictionary<string, object> 单个事务需要修改的的配置项集合.使用字典的好处是如果在同一个事务中多次修改同一个配置项的值,会以最后一次为准!
        private readonly ConcurrentDictionary<long, LightWeightDictionary> _transactionCache;

        private long _id = 0;

        public IConfigSourceWriter BeginTransaction(out long transactionId)
        {
            // _id: 0,1,2...long.max,long.min(overflow),long.min + 1...0...long.max...如此循环往复.
            transactionId = Interlocked.Increment(ref _id);
            _transactionCache.TryAdd(transactionId, new LightWeightDictionary());
            return this;
        }

        public void CommitTransaction(long transactionId)
        {
            // 即使Set(会校验新值合法性以及写磁盘)失败,事务也会从缓存中移除,不会永驻导致内存泄漏.
#if NET10_0_OR_GREATER
            if (_transactionCache.Remove(transactionId, out var configs))
                Set(configs);
#elif NET462_OR_GREATER
            if (_transactionCache.TryRemove(transactionId, out var configs))
                Set(configs);
#endif
            throw new InvalidOperationException($"This transaction '{transactionId}' has not been created or committed");
        }

        public IConfigSourceWriter Set(long transactionId, string config, object value)
        {
            if (_transactionCache.TryGetValue(transactionId, out LightWeightDictionary configs))
            {
                // 在一个事务中多次修改同一配置的值,后面的覆盖前面的,最终的值是最后一次的赋值.
                configs.AddOrUpdate(config, value);
            }
            else
            {
                throw new InvalidOperationException($"Try to set the value on a non-existent transaction(id={transactionId}).");
            }
            return this;
        }

        private void Set(LightWeightDictionary configValuePairs)
        {
            if (configValuePairs == null)
            {
                throw new ArgumentNullException(nameof(configValuePairs));
            }
            if (configValuePairs.Count() == 0)
            {
                throw new ArgumentException("At least one config item must be provided.", nameof(configValuePairs));
            }
            if (configValuePairs.Any(x => string.IsNullOrWhiteSpace(x.Key)))
            {
                throw new ArgumentException("Config name cannot be null or empty.", nameof(configValuePairs));
            }

            var configItems = _configItems;

            foreach (var configValue in configValuePairs)
            {
                if (!configItems.ContainsKey(configValue.Key))
                {
                    throw new KeyNotFoundException($"{configValue.Key} not found in the config collection.");
                }
            }

            // 将新值转换成字符串形式
            var configStringValuePairs = new List<(string configItem, string value)>(configValuePairs.Count());
            foreach (var item in configValuePairs)
            {
                configStringValuePairs.Add((item.config, Convert2String(item.value)));
            }

            // 去除新值与旧值相等的配置项
            for (int i = configStringValuePairs.Count - 1; i >= 0; i--)
            {
                if (configItems[configStringValuePairs[i].configItem].StringValue == configStringValuePairs[i].value)
                {
                    configStringValuePairs.RemoveAt(i);
                }
            }
            // 如果所有的更改都是新值与旧值相同,那么就没必要写入磁盘了,这种设计避免了用户连续多次点击Save按钮导致频繁磁盘IO的情况.
            if (configStringValuePairs.Count > 0)
            {
                // 有一项非法值会导致全部失败,不会出现部分配置项修改部分没改,具备原子性.
                ValidateValue(configStringValuePairs);

                List<(string configItem, string oldValue, string newValue)> changedListForInvoke = new List<(string configItem, string oldValue, string newValue)>();

                List<KeyValuePair<string, ConfigItem>> changedListForDict = new List<KeyValuePair<string, ConfigItem>>();

                foreach (var item in configStringValuePairs)
                {
                    changedListForInvoke.Add((item.configItem, configItems[item.configItem].StringValue, item.value));
                    var changingItem = ((ICloneable)(configItems[item.configItem])).Clone() as ConfigItem;
                    changingItem.StringValue = item.value;
                    changingItem.ObjectValue = Convert2Object(changingItem.Type, item.value);
                    changedListForDict.Add(new KeyValuePair<string, ConfigItem>(item.configItem, changingItem));
                }
                // Copy-On-Write
                (_configItems as ImmutableDictionary<string, ConfigItem>).SetItems(changedListForDict);
                // 触发事件,一般用于日志记录
                ValueSet?.Invoke(changedListForInvoke);
                // 异步刷盘
                if (_channel.Writer.TryWrite(configStringValuePairs) == false)
                {
                    throw new Exception("Failed to add to the asynchronous persistence to disk queue.");
                }
            }
        }
       

    }
}