using System;
using System.Collections.Specialized;
using SCADA.Common;

namespace SCADA.Events
{
    public class EventInstance
    {
        public EventDef EventDef { get; internal set; }
        public long Id { get; internal set; }
        public DateTime OccurTime { get; internal set; }
        public string Source { get; internal set; }

        public ListDictionary DvidValues { get; internal set; }
    }
}
