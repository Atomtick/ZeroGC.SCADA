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
            string path = Path.GetFullPath("configs.db");
            IConfigValidator configSource = new PrimitiveConfigSource(path);


            configSource.ValidateValue("FA.IsEnabled", "true");
            configSource.ValidateValue("FA.IsEnabled", "True");
            configSource.ValidateValue("FA.IsEnabled", "TRUE");
            configSource.ValidateValue("FA.IsEnabled", "tRuE");
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.IsEnabled", "Truee"));
            configSource.ValidateValue("FA.IsEnabled", "false");
            configSource.ValidateValue("FA.IsEnabled", "False");
            configSource.ValidateValue("FA.IsEnabled", "FALSE");
            configSource.ValidateValue("FA.IsEnabled", "fAlsE");
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.IsEnabled", "Falsee"));


            configSource.ValidateValue("FA.LocalIpAddress", "192.168.0.36");
            configSource.ValidateValue("FA.LocalIpAddress", "0.0.0.0");
            configSource.ValidateValue("FA.LocalIpAddress", "127.0.0.1");
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalIpAddress", "192.168.0.256"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalIpAddress", "192.168.0.2."));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalIpAddress", "192.168.0..2"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalIpAddress", ".192.168.0.2"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalIpAddress", ".168.0.254"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalIpAddress", "168.0.254"));


            configSource.ValidateValue("FA.LocalPortNumber", "7000");
            configSource.ValidateValue("FA.LocalPortNumber", "7100");
            configSource.ValidateValue("FA.LocalPortNumber", "7033");
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalPortNumber", "6999"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalPortNumber", "7101"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalPortNumber", "7033.0"));
            Assert.ThrowsAny<ArgumentException>(() => configSource.ValidateValue("FA.LocalPortNumber", "703a"));

        }
    }
}
