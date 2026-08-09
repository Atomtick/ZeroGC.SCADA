using Atomtick.Common;
using System;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Events.REPTID
{
    public class ReportManger
    {
        private ReportManger()
        { }

        public static ReportManger Instance { get; private set; } = new ReportManger();

#if NET462_OR_GREATER
        private volatile IDictionary<long, Report> _dict = new System.Collections.Generic.Dictionary<long, Report>();
#elif NET8_0_OR_GREATER
        volatile IDictionary<long, Report> _dict = new System.Collections.Generic.Dictionary<long, Report>().ToFrozenDictionary();
#endif

        public Func<long, bool> CheckVidPresent;
        private object _lock = new object();

        public void Clear()
        {
            lock (_lock)
            {
#if NET462_OR_GREATER
                var dict = new Dictionary<long, Report>();
#elif NET8_0_OR_GREATER
                var dict = new Dictionary<long, Report>().ToFrozenDictionary();
#endif
                _dict = dict;
            }
        }

        public bool Register(IList<Report> reportDefs, out IList<long> absentVids)
        {
            absentVids = new List<long>();
            lock (_lock)
            {
                if (reportDefs == null || reportDefs.Count == 0)
                {
                    return true;
                }

                var copy = new Dictionary<long, Report>();
                foreach (var item in _dict)
                {
                    copy.Add(item.Key, item.Value.Clone() as Report);
                }

                foreach (var item in copy)
                {
                    foreach (var vid in item.Value.Vids)
                    {
                        if (CheckVidPresent?.Invoke(vid) == false)
                        {
                            absentVids.Add(vid);
                        }
                    }
                }

                List<long> removes = new List<long>();
                foreach (var item in copy)
                {
                    if (item.Value.Vids == null || item.Value.Vids.Count() == 0)
                    {
                        removes.Add(item.Key);
                    }
                }

                foreach (var item in removes)
                {
                    copy.Remove(item);
                }

#if NET8_0_OR_GREATER
                _dict = copy.ToFrozenDictionary();
#endif

                return absentVids.Count == 0;
            }
        }

        public bool GetVids(long reportId, out long[] vids)
        {
            var dict = _dict;
            var exists = dict.TryGetValue(reportId, out var report);
            vids = report.Vids;
            return exists;
        }
    }
}