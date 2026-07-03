using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.PLCFramework
{
    public readonly struct ReadResult<T>
    {
        public ReadResult(long timestamp, T value, bool isError)
        {
            Timestamp = timestamp;
            Value = value;
            IsError = isError;
        }

        public long Timestamp { get; }
        public T Value { get; }
        public bool IsError { get; }
    }

    public readonly struct ReadResult<T1, T2>
    {
        public ReadResult(ReadResult<T1> result1, ReadResult<T2> result2, bool isError)
        {
            Result1 = result1;
            Result2 = result2;
            IsError = isError;
        }

        public ReadResult<T1> Result1 { get; }
        public ReadResult<T2> Result2 { get; }
        public bool IsError { get; }
    }

    public readonly struct ReadResult<T1, T2, T3>
    {
        public ReadResult(ReadResult<T1> result1, ReadResult<T2> result2, ReadResult<T3> result3, bool isError)
        {
            Result1 = result1;
            Result2 = result2;
            Result3 = result3;
            IsError = isError;
        }

        public ReadResult<T1> Result1 { get; }
        public ReadResult<T2> Result2 { get; }
        public ReadResult<T3> Result3 { get; }
        public bool IsError { get; }
    }

    public readonly struct ReadResult<T1, T2, T3, T4>
    {
        public ReadResult(ReadResult<T1> result1, ReadResult<T2> result2, ReadResult<T3> result3, ReadResult<T4> result4, bool isError)
        {
            Result1 = result1;
            Result2 = result2;
            Result3 = result3;
            Result4 = result4;
            IsError = isError;
        }

        public ReadResult<T1> Result1 { get; }
        public ReadResult<T2> Result2 { get; }
        public ReadResult<T3> Result3 { get; }
        public ReadResult<T4> Result4 { get; }
        public bool IsError { get; }
    }

    public readonly struct ReadResult<T1, T2, T3, T4, T5>
    {
        public ReadResult(ReadResult<T1> result1, ReadResult<T2> result2, ReadResult<T3> result3, ReadResult<T4> result4, ReadResult<T5> result5, bool isError)
        {
            Result1 = result1;
            Result2 = result2;
            Result3 = result3;
            Result4 = result4;
            Result5 = result5;
            IsError = isError;
        }

        public ReadResult<T1> Result1 { get; }
        public ReadResult<T2> Result2 { get; }
        public ReadResult<T3> Result3 { get; }
        public ReadResult<T4> Result4 { get; }
        public ReadResult<T5> Result5 { get; }
        public bool IsError { get; }
    }

    public class ReadResult
    {
        public class Entity
        {
            public long timestamp { get; set; }
            public double value { get; set; }
        }

        public bool IsError { get; }
        public Dictionary<string, Entity> Entities { get; set; }

        //public (long timestamp,T value) GetValue<T>(string name)
        //{
            
        //}
    }
}