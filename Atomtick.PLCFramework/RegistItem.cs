using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.PLCFramework
{
    internal sealed class RegistItem
    {
        public RegistItem(string name, string index, ValueType type, string display, string desc, string blockId)
        {
            Name = name;
            Index = index;
            Type = type;
            Desc = desc;
            Display = display;
            BlockId = blockId;
            ByteIndex = -1;
            BitIndex = -1;
            int dotIndex = index.IndexOf('.');
            if (dotIndex != -1)
            {
                ByteIndex = int.Parse(index.Substring(0, dotIndex));
                BitIndex = int.Parse(index.Substring(dotIndex + 1));
            }
            else
            {
                ByteIndex = int.Parse(index);
            }
        }

        public string Name { get; }
        public string Index { get; }
        public string Display { get; }
        public ValueType Type { get; }
        public string BlockId { get; }
        public string Desc { get; }

        internal int ByteIndex { get; set; }
        internal int BitIndex { get; set; }
    }
}