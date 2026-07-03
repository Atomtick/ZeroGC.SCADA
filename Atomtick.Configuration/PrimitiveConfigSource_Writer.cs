using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SCADA.Common;
using SCADA.Common.Interfaces;
using SCADA.Configuration.Interfaces;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource : IConfigWriter
    {
        // long 事务id,允许多个事务并行.
        // ConcurrentDictionary<string, object> 单个事务需要修改的的配置项集合.使用字典的好处是如果在同一个事务中多次修改同一个配置项的值,会以最后一次为准!
        private readonly ConcurrentDictionary<long, LightWeightMap> _transactionCache;
        private readonly List<string> _equalsKeys = new List<string>();
        private long _id;

        public IConfigWriter BeginTransaction(out long transactionId)
        {
            // _id: 0,1,2...long.max,long.min(overflow),long.min + 1...0...long.max...如此循环往复.
            transactionId = Interlocked.Increment(ref _id);
            _transactionCache.TryAdd(transactionId, new LightWeightMap());
            return this;
        }

        public void CommitTransaction(long transactionId)
        {
            // 即使Save失败,事务也会从缓存中移除,不会永驻导致内存泄漏.
#if NET8_0_OR_GREATER
            if (_transactionCache.Remove(transactionId, out var configs))
                Save(configs);
#elif NET462_OR_GREATER
            if (_transactionCache.TryRemove(transactionId, out var configs))
                Save(configs);
#endif
            else
                throw new InvalidOperationException($"This transaction '{transactionId}' has not been created or committed");
        }

        public IConfigWriter Write(long transactionId, string config, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Config value cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentException("Config name cannot be null or empty.", nameof(config));
            }

            if (!_configItems.ContainsKey(config))
            {
                throw new KeyNotFoundException($"{config} not found in the config collection.");
            }

            ValidateValue(config, value);

            if (_transactionCache.TryGetValue(transactionId, out LightWeightMap configs))
            {
                if (value is string stringValue)
                {
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        throw new ArgumentException("Config value cannot be null or empty.", nameof(value));
                    }
                }
                else
                {
                    value = Convert2String(value);
                }
                configs.AddOrUpdate(config, value);
            }
            else
            {
                throw new InvalidOperationException($"Try to write the value of '{config}' on a non-existent transaction(id='{transactionId}').");
            }

            return this;
        }

        private void Save(LightWeightMap modificationConfigs)
        {
            if (modificationConfigs != null && modificationConfigs.Any())
            {
                // 剔除值没变化的配置项
                _equalsKeys.Clear();
                foreach (var pair in modificationConfigs.Where(pair => (pair.Value as string) == _configItems[pair.Key].StringValue))
                {
                    _equalsKeys.Add(pair.Key);
                }
                foreach (var item in _equalsKeys)
                {
                    modificationConfigs.Remove(item);
                }
                _equalsKeys.Clear();

                // 存在新值与旧值不相等的情况才会触发修改动作
                if (modificationConfigs.Any())
                {
                    // 把数据提前准备好,以保证锁内只有最小的代码量(只有极其简单的赋值操作),提高并发性能.因为锁内的代码越少,并发性能越高.
                    string[] stringValues = new string[modificationConfigs.Count];
                    object[] objectValues = new object[modificationConfigs.Count];
                    ConfigItem[] configItems = new ConfigItem[modificationConfigs.Count];
                    int index = 0;
                    foreach (var pair in modificationConfigs)
                    {
                        configItems[index] = _configItems[pair.Key];
                        stringValues[index] = pair.Value as string;
                        objectValues[index] = Convert2Object(_configItems[pair.Key].Type, pair.Value as string);
                        ++index;
                    }
                    _seqLock.WriteLock();
                    for (int i = 0; i < modificationConfigs.Count; i++)
                    {
                        configItems[i].StringValue = stringValues[i];
                        configItems[i].ObjectValue = objectValues[i];
                    }
                    _seqLock.WriteUnlock();
                    // 异步刷盘
                    if (_channel.Writer.TryWrite(modificationConfigs) == false)
                    {
                        throw new Exception("Failed to add to the asynchronous persistence to disk queue.");
                    }
                    ValueChanged?.Invoke(modificationConfigs);
                }
            }
        }
    }
}
