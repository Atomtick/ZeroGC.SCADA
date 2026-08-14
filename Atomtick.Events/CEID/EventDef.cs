using System;
using SCADA.Common;

namespace Atomtick.Events.CEID
{
    public sealed class EventDef
    {
        public EventDef(int id, string name, EventLevel level, string descriptionTemplate, bool enabled)
        {
            Id = id;
            Name = name;
            Level = level;
            DescriptionTemplate = descriptionTemplate;
            Enabled = enabled;
        }

        public int Id { get; }
        public string Name { get; }
        public EventLevel Level { get; internal set; }
        public string DescriptionTemplate { get; }
        public bool Enabled { get; internal set; }
    }
}
