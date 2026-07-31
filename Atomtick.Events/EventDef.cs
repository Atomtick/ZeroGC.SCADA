using SCADA.Common;
using System;

namespace SCADA.Events
{

    public sealed class EventDef 
    {
        public EventDef(int id, string name, EventLevel level, string description, bool enabled)
        {
            Id = id;
            Name = name;
            Level = level;
            Description = description;
            Enabled = enabled;
        }

        public int Id { get;  }
        public string Name { get;  }
        public EventLevel Level { get;  }
        public string Description { get; internal set; }
        public bool Enabled { get; internal set; }

        public EventDef Clone()
        {
            return new EventDef(Id, Name, Level, Description, Enabled);
        }
    }
}