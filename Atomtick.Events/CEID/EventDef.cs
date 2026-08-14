using System;

namespace Atomtick.Events.CEID
{
    public sealed class EventDef
    {
        public EventDef(long id, string name, EventLevel level, string descriptionTemplate, bool enabled)
        {
            Id = id;
            Name = name;
            Level = level;
            DescriptionTemplate = descriptionTemplate;
            Enabled = enabled;
        }

        public long Id { get; }
        public string Name { get; }
        public EventLevel Level { get; internal set; }
        public string DescriptionTemplate { get; }
        public bool Enabled { get; internal set; }
    }
}
