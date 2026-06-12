using SCADA.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource : IConfigWriter
    {
        // long 事务id,允许多个事务并行.
        // ConcurrentDictionary<string, object> 单个事务需要修改的的配置项集合.使用字典的好处是如果在同一个事务中多次修改同一个配置项的值,会以最后一次为准!
        private readonly ConcurrentDictionary<long, LightWeightDictionary> _transactionCache;

        private readonly List<string> _equalsKeys = new List<string>();
        private long _id;

        public IConfigWriter BeginTransaction(out long transactionId)
        {
            // _id: 0,1,2...long.max,long.min(overflow),long.min + 1...0...long.max...如此循环往复.
            transactionId = Interlocked.Increment(ref _id);
            _transactionCache.TryAdd(transactionId, new LightWeightDictionary());
            return this;
        }

        public void CommitTransaction(long transactionId)
        {
            // 即使Set(会校验新值合法性以及写磁盘)失败,事务也会从缓存中移除,不会永驻导致内存泄漏.
#if NET8_0_OR_GREATER
            if (_transactionCache.Remove(transactionId, out var configs))
                Save(configs);
#elif NET462_OR_GREATER
            if (_transactionCache.TryRemove(transactionId, out var configs))
                Save(configs);
#endif
            throw new InvalidOperationException($"This transaction '{transactionId}' has not been created or committed");
        }

        public IConfigWriter Write(long transactionId, string config, object value)
        {
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new ArgumentException("Config name cannot be null or empty.", nameof(config));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Config value cannot be null.");
            }

            var configItems = _configItems;
            if (!configItems.ContainsKey(config))
            {
                throw new KeyNotFoundException($"{config} not found in the config collection.");
            }

            ValidateValue(config, value);

            if (_transactionCache.TryGetValue(transactionId, out LightWeightDictionary configs))
            {
                // 把object类型的值转换成字符串形式,在Set方法中进行合法性校验,如果不合法就抛异常,保证事务中的所有配置项都合法才会真正修改配置并写磁盘,具备原子性.如果在这里就修改了_configItems中的配置项的值,一旦有一个配置项的值不合法抛异常了,就会导致部分配置项被修改了值,部分配置项没有被修改值的情况发生,不具备原子性.
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

                // 在一个事务中多次修改同一配置的值,后面的覆盖前面的,最终的值是最后一次的赋值.
                configs.AddOrUpdate(config, value);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Try to set the value on a non-existent transaction(id={transactionId}).");
            }

            return this;
        }

        private void Save(LightWeightDictionary modificationConfigs)
        {
            if (modificationConfigs != null && modificationConfigs.Any())
            {
                _seqLock.WriteLock();

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

                if (modificationConfigs.Any())
                {
                    foreach (var pair in modificationConfigs)
                    {
                        _configItems[pair.Key].StringValue = pair.Value as string;
                        _configItems[pair.Key].ObjectValue =
                            Convert2Object(_configItems[pair.Key].Type, pair.Value as string);
                    }
                }

                _seqLock.WriteUnlock();

                if (modificationConfigs.Any())
                {
                    // 异步刷盘
                    if (_channel.Writer.TryWrite(modificationConfigs) == false)
                    {
                        throw new Exception("Failed to add to the asynchronous persistence to disk queue.");
                    }

                    // 触发事件,一般用于日志记录
                    ValueChanged?.Invoke(modificationConfigs);
                }
            }
        }
    }
}