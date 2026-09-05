using Npgsql;
using NpgsqlTypes;
using System.Data.Common;

namespace ConsoleApp1
{
    public struct EventInstance
    {
        private string _source;
        public long Id { get; internal set; }
        public DateTime OccurTime { get; internal set; }
        public string Source
        {
            get => _source;
            internal set
            {
                _source = value;
                var index = _source.IndexOf('.');
                Module = index == -1 ? value : _source.Substring(0, index);
            }
        }
        public string Module { get; internal set; }
        public string Description { get; internal set; }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
             var connection =  new NpgsqlConnection();

            // 1. 开启二进制 COPY 导入
            using var writer =  connection.BeginBinaryImport(
                "COPY my_table (id, name, created_at, value) FROM STDIN (FORMAT BINARY)"
            );

            EventInstance[] myDataCollection = new EventInstance[100];
            foreach (var item in myDataCollection)
            {
                // 2. 开始新的一行
                 writer.StartRow();

                // 3. 使用强类型泛型 Write<T> 方法写入，彻底避免装箱 (Boxing)
                // 注意：务必指定 NpgsqlDbType 以避免 Npgsql 内部的类型推断开销
                 writer.Write<long>(item.Id, NpgsqlDbType.Bigint);

                // 字符串属于引用类型，如果可能重复，考虑使用字符串常量或池化技术
                 writer.Write<IList<Byte>>(new byte[0], NpgsqlDbType.Bytea);

                 writer.Write<DateTime>(item.OccurTime, NpgsqlDbType.TimestampTz);

                // 值类型 (struct) 在这里完全不会产生 GC
                 writer.Write<double>(item.Description, NpgsqlDbType.Double);
            }

            // 4. 提交写入（必须调用，否则数据会丢失）
             writer.Complete();
        }
    }
}
