using System;

namespace SCADA.Events
{
    public class EventItem
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public EventLevel Level { get; set; }
        public string Name { get; set; }
        public NotifyType NotifyType { get; set; }
        public DateTime OccurTime { get; set; }
        public string Source { get; set; }
        public string UserName { get; set; }
    }
}