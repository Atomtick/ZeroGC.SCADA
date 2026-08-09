using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Events
{
    public class CEIDLinks
    {
        private CEIDLinks() { }
        public static CEIDLinks Instance {  get; private set; } = new CEIDLinks();


    }
}
