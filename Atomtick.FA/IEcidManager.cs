using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.FA
{
    public interface IEcidManager
    {
        Dictionary<string, long> GetAllEcids();
        long GenerateEcid(string statusVar);
    }
}
