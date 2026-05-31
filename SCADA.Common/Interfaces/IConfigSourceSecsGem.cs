using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Common.Interfaces
{
    public interface IConfigSourceSecsGem
    {
        IConfigItem[] Get(string[] configNames);

        IConfigItem Get(string config);

        (IConfigItem, IConfigItem)
            Get(string config1, string config2);

        (IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10, string config11);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10, string config11, string config12);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
                Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10, string config11, string config12, string config13);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10, string config11, string config12, string config13, string config14);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10, string config11, string config12, string config13, string config14, string config15);

        (IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem, IConfigItem)
            Get(string config1, string config2, string config3, string config4, string config5, string config6, string config7, string config8, string config9, string config10, string config11, string config12, string config13, string config14, string config15, string config16);
    }
}
