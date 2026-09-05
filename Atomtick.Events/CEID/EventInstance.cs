using Atomtick.Common;
using CommunityToolkit.HighPerformance.Buffers;
using SCADA.Common;
using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

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
                if(index == -1)
                {
                    Module = value;
                }
                else
                {
                    ReadOnlySpan<char> spanKey = _source.AsSpan(0, index + 1);
                    Module = index == -1 ? value : StringPool.Shared.GetOrAdd(spanKey);
                }
            }
        }
        public string Module { get; internal set; }
        public string Description { get; internal set; }
        public IReadonlyListDict DvidValues { get; internal set; }
    }
}
