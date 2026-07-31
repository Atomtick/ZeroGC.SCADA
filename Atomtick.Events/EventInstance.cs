using SCADA.Common;
using System;

namespace SCADA.Events
{
    public class EventInstance
    {
        public EventDef EventDef { get; set; }
        public long Id { get; set; }
        public DateTime OccurTime { get; set; }
        public string Source { get; set; }
     
        public LightWeightMap DvidValues { get; set; }
    }
}