using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace Atomtick.Common
{
    public sealed class DoubleKeyValuePairs<TKey1, TKey2, TValue>
    {
        public TKey1 Key1 { get; }
        public TKey2 Key2 { get; }
        public TValue Value { get; }

        public DoubleKeyValuePairs(TKey1 key1, TKey2 key2, TValue value)
        {
            Key1 = key1;
            Key2 = key2;
            Value = value;
        }
    }

    // 将两个字典封装成一个不可变的快照节点
    public sealed class DoubleKeyDictionary<TKey1, TKey2, TValue>
    {
        private sealed class Snapshot
        {
            public readonly IDictionary<TKey1, TValue> DictByKey1;
            public readonly IDictionary<TKey2, TValue> DictByKey2;

            public Snapshot(Dictionary<TKey1, TValue> byKey1, Dictionary<TKey2, TValue> byKey2)
            {
#if NET462_OR_GREATER
                DictByKey1 = byKey1;
                DictByKey2 = byKey2;
#elif NET8_0_OR_GREATER
                DictByKey1 = byKey1.ToFrozenDictionary();
                DictByKey2 = byKey2.ToFrozenDictionary();
#endif
            }
        }

        // volatile 保证多线程可见性
        private volatile Snapshot _snapshot;

        private readonly object _writeLock;

        public DoubleKeyDictionary()
        {
            _writeLock = new object();
            _snapshot = new Snapshot(new Dictionary<TKey1, TValue>(), new Dictionary<TKey2, TValue>());
        }

        // 读操作 1：完全无锁！直接获取快照并读取
        public bool GetByKey1(TKey1 key1, out TValue value)
        {
            var snap = _snapshot; // 拿到当前瞬间的快照引用
            return snap.DictByKey1.TryGetValue(key1, out value);
        }

        // 读操作 2：完全无锁！
        public bool GetByKey2(TKey2 key2, out TValue value)
        {
            var snap = _snapshot;
            return snap.DictByKey2.TryGetValue(key2, out value);
        }

        // 添加操作：加锁 -> 深度复制字典 -> 修改 -> 替换快照
        public bool Add(TKey1 key1, TKey2 key2, TValue item)
        {
            lock (_writeLock)
            {
                var oldSnap = _snapshot;

                bool exists1 = oldSnap.DictByKey1.ContainsKey(key1);
                bool exists2 = oldSnap.DictByKey2.ContainsKey(key2);
                bool exists = exists1 || exists2;
                if (exists1)
                {
                    return false;
                }
                else
                {
                    // 复制旧字典（因为是引用类型，仅复制引用，速度很快）
                    var newDict1 = new Dictionary<TKey1, TValue>(oldSnap.DictByKey1);
                    var newDict2 = new Dictionary<TKey2, TValue>(oldSnap.DictByKey2);

                    newDict1[key1] = item;
                    newDict2[key2] = item;

                    // 原子替换：旧快照被丢弃，等待 GC 回收
                    _snapshot = new Snapshot(newDict1, newDict2);
                    return true;
                }
            }
        }

        // 添加操作：加锁 -> 深度复制字典 -> 修改 -> 替换快照
        public bool Add(IEnumerable<DoubleKeyValuePairs<TKey1, TKey2, TValue>> pairs, out TKey1 duplicateKey1, out TKey2 duplicateKey2)
        {
            lock (_writeLock)
            {
                var oldSnap = _snapshot;

                foreach (var item in pairs)
                {
                    bool exists1 = oldSnap.DictByKey1.ContainsKey(item.Key1);
                    bool exists2 = oldSnap.DictByKey2.ContainsKey(item.Key2);
                    bool exists = exists1 || exists2;
                    if (exists1)
                    {
                        duplicateKey1 = item.Key1;
                        duplicateKey2 = item.Key2;
                        return false;
                    }
                }

                // 复制旧字典（因为是引用类型，仅复制引用，速度很快）
                var newDict1 = new Dictionary<TKey1, TValue>(oldSnap.DictByKey1);
                var newDict2 = new Dictionary<TKey2, TValue>(oldSnap.DictByKey2);

                foreach (var pair in pairs)
                {
                    newDict1[pair.Key1] = pair.Value;
                    newDict2[pair.Key2] = pair.Value;
                }

                // 原子替换：旧快照被丢弃，等待 GC 回收
                _snapshot = new Snapshot(newDict1, newDict2);
                duplicateKey1 = default;
                duplicateKey2 = default;
                return true;
            }
        }

        // throw new ArgumentException($"Duplicate key '{item.Key1}' detected.");
        public bool Remove(TKey1 key1, TKey2 key2)
        {
            lock (_writeLock)
            {
                var oldSnap = _snapshot;
                bool b1 = oldSnap.DictByKey1.ContainsKey(key1);
                bool b2 = oldSnap.DictByKey2.ContainsKey(key2);
                bool b = b1 && b2;
                if (b)
                {
                    var newDict1 = new Dictionary<TKey1, TValue>(oldSnap.DictByKey1);
                    var newDict2 = new Dictionary<TKey2, TValue>(oldSnap.DictByKey2);
                    newDict1.Remove(key1);
                    newDict2.Remove(key2);
                    _snapshot = new Snapshot(newDict1, newDict2);
                }
                return b;
            }
        }
    }
}
