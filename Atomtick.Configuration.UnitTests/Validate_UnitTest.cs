using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atomtick.Configuration.Interfaces;
using SCADA.Configuration;

namespace Atomtick.Configuration.UnitTests
{
    public class Validate_UnitTest
    {
        [Fact]
        public void Test()
        {
            string path = Path.GetFullPath("../../../../Atomtick.Configuration.Benchmarks/configs.db");
            IConfigValidator configSource = new PrimitiveConfigSource(path);

            configSource.ValidateValue("FA.LocalPortNumber", "1000");
            configSource.ValidateValue("TM.DryPump.DryPumpType", "EbaraS20P");
            configSource.ValidateValue("FA.Enable", "true");

            Assert.ThrowsAny<Exception>(() => configSource.ValidateValue("FA.LocalPortNumber", "1000M"));
            Assert.True(configSource.ValidateValue("FA.LocalPortNumber", "1000", out string errorMessage));
            Assert.False(configSource.ValidateValue("FA.LocalPortNumber", "1000M", out errorMessage));
        }
    }
}
