using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using Atomtick.Common;
using SCADA.Common;

namespace Atomtick.Events.CEID
{
    public struct EventInstance
    {
        private string _source;

        public EventDef EventDef { get; internal set; }
        public long Id { get; internal set; }
        public DateTime OccurTime { get; internal set; }
        public string Source
        {
            get => _source;
            internal set
            {
                _source = value;
                var index = _source.IndexOf('.');
                Module = index == -1 ? value : _source.Substring(0, index);
            }
        }
        public string Module { get; internal set; }
        public string Description { get; internal set; }
        public IReadonlyListDict DvidValues { get; internal set; }
    }
}
