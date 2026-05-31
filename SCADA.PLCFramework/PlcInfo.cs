using SCADA.Common;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.PLCFramework
{
    internal sealed class PlcInfo
    {
        public string Module { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Class { get; set; }
        public string Assembly { get; set; }
        public int IOValueBufferCount { get; set; } = 5;
        public FrozenDictionary<string, Block> Blocks { get; set; }
        public FrozenDictionary<string, RegistItem> DIs { get; set; }
        public FrozenDictionary<string, RegistItem> DOs { get; set; }
        public FrozenDictionary<string, RegistItem> AIs { get; set; }
        public FrozenDictionary<string, RegistItem> AOs { get; set; }
        public Dictionary<InterlockAction, InterlockLimit[]> ActionLimitsDict { get; set; }
    }
}
