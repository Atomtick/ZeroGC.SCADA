using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Events.REPTID
{
    public class Report :ICloneable
    {
        public Report()
        {
            Vids = new long[0];
        }

        public long Id { get; private set; }
        public long[] Vids { get; private set; }
        
        public object Clone()
        {
            var @new = new Report();
            @new.Id = Id;
            @new.Vids = new long[Vids.Length];
            return @new;
        }
    }
}
