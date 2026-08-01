using System;
using SCADA.Common;

namespace SCADA.Events
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
        public EventLevel Level { get; }
        public string DescriptionTemplate { get; }
        public bool Enabled { get; internal set; }
    }
}
