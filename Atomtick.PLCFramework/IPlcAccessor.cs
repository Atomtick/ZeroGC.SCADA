using SCADA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.PLCFramework
{
    public interface IPlcAccessor
    {
        // 返回时间戳
        ReadResult<T> Read<T>(string name) where T : IConvertible, IComparable;
        ReadResult<T1, T2> Read<T1, T2>(string name1, string name2) where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable;
        ReadResult<T1, T2, T3> Read<T1, T2, T3>(string name1, string name2, string name3) where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable where T3 : IConvertible, IComparable;
        ReadResult<T1, T2, T3, T4> Read<T1, T2, T3, T4>(string name1, string name2, string name3, string name4) where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable where T3 : IConvertible, IComparable where T4 : IConvertible, IComparable;
        ReadResult<T1, T2, T3, T4, T5> Read<T1, T2, T3, T4, T5>(string name1, string name2, string name3, string name4, string name5) where T1 : IConvertible, IComparable where T2 : IConvertible, IComparable where T3 : IConvertible, IComparable where T4 : IConvertible, IComparable where T5 : IConvertible, IComparable;
        ReadResult Read(params string[] names);

        // write写到队列中，返回写入队列的id
        // 查询id是否还在字典中,如果在，说明还没写到PLC，返回false
        long Write<T>(string name, T value);

        long Write<T1, T2>(string name1, T1 value1, string name2, T2 value2);

        long Write<T1, T2, T3>(string name1, T1 value1, string name2, T2 value2, string name3, T3 value3);

        long Write<T1, T2, T3, T4>(string name1, T1 value1, string name2, T2 value2, string name3, T3 value3, string name4, T4 value4);

        long Write<T1, T2, T3, T4, T5>(string name1, T1 value1, string name2, T2 value2, string name3, T3 value3, string name4, T4 value4, string name5, T5 value5);

        long Write(params (string name, object value)[] nameValues);

        bool IsWriteCompleted(long id);
    }
}