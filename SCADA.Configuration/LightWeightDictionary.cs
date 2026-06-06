using System;
using System.Collections;
using System.Collections.Generic;

namespace SCADA.Configuration
{
   public class LightWeightDictionary : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// 当前容器内元素的个数
        /// </summary>
        private int _count;

        private Entry[] _entries;

        public LightWeightDictionary(int initialCapacity = 16)
        {
            _entries = new Entry[initialCapacity];
        }

        public int Count => _count;

        public object this[string key]
        {
            get
            {
                for (int i = 0; i < _count; i++)
                {
                    if (ReferenceEquals(_entries[i].Key, key) ||
                        string.Equals(_entries[i].Key, key, StringComparison.Ordinal))
                    {
                        return _entries[i].Value;
                    }
                }
                return null;
            }
        }

        public void AddOrUpdate(string key, object value)
        {
            // 如果Key已经存在则覆盖
            for (int i = 0; i < _count; i++)
            {
                if (ReferenceEquals(_entries[i].Key, key) ||
                    string.Equals(_entries[i].Key, key, StringComparison.Ordinal))
                {
                    _entries[i].Value = value;
                    return;
                }
            }
            // 超出容量后进行扩容
            if (_count >= _entries.Length)
            {
                Array.Resize(ref _entries, _entries.Length * 2);
            }
            // 添加新的键值对
            _entries[_count].Key = key;
            _entries[_count].Value = value;
            _count++;
        }

        public void Remove(string key)
        {
            for (int i = 0; i < _count; i++)
            {
                if (ReferenceEquals(_entries[i].Key, key) ||
                    string.Equals(_entries[i].Key, key, StringComparison.Ordinal))
                {
                    // 将最后一个元素移动到当前位置覆盖被删除的元素
                    _entries[i] = _entries[_count - 1];
                    _entries[_count - 1] = default; // 清除最后一个元素的引用
                    _count--;
                    return;
                }
            }
        }

        public void Clear()
        {
            if (_count > 0)
            {
                Array.Clear(_entries, 0, _count);
                _count = 0;
            }
        }

        // 1. 提供给 foreach 使用的高性能入口，返回 struct 避免装箱和 GC 分配
        public Enumerator GetEnumerator() => new Enumerator(this);

        // ==========================================
        // 迭代器实现部分
        // ==========================================
        // 2. 显式实现接口，提供对 LINQ 的兼容性支持
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // 嵌套的 struct 枚举器，可以直接访问外部类的私有字段 _entries 和 _count
        public struct Enumerator : IEnumerator<KeyValuePair<string, object>>
        {
            private readonly LightWeightDictionary _dict;
            private int _index;

            internal Enumerator(LightWeightDictionary dict)
            {
                _dict = dict;
                _index = -1; // 初始状态游标在第一个元素之前
            }

            // 返回标准库的 KeyValuePair，方便解构和使用
            public KeyValuePair<string, object> Current
            {
                get
                {
                    // 为了极致性能，不在这里做越界检查，依赖 MoveNext 的保护
                    var entry = _dict._entries[_index];
                    return new KeyValuePair<string, object>(entry.Key, entry.Value);
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                // 没有任何非托管资源需要释放，留空即可
            }

            public bool MoveNext()
            {
                if (_index + 1 < _dict._count)
                {
                    _index++;
                    return true;
                }
                return false;
            }

            public void Reset() => _index = -1;
        }

        private struct Entry
        {
            public string Key;
            public object Value;
        }
    }
}