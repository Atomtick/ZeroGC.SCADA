using System;
using System.Collections.Generic;
using System.Text;

namespace SCADA.PLCFramework
{
    // PLC写操作不是高频操作,所以可以是class
    internal sealed class WriteData
    {
        public WriteData(long iD, IList<Entity> entities)
        {
            ID = iD;
            Entities = entities;
        }

        public IList<Entity> Entities { get; set; }

        public long ID { get; }

        public class Entity
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }
    }
}
