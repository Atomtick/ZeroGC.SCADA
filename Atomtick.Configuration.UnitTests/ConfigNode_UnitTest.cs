using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atomtick.Configuration.Interfaces;

namespace Atomtick.Configuration.UnitTests
{
    public class ConfigNode_UnitTest
    {
        [Fact]
        public void Test()
        {
            string path = Path.GetFullPath("../../../../Atomtick.Configuration.Benchmarks/configs.db");
            PrimitiveConfigSource configSource = new PrimitiveConfigSource(path);

            Assert.True(
                ConfigNode.FindByItemPath("PM1.SourceRFPower.Injet.EnableLogMessage", configSource.RootNodes.First(x => x.Name == "PM1"), out ConfigItem configItem, out ConfigNode configNode)
            );
            Assert.True(ConfigNode.FindByNodePath("PM1.SourceRFPower", configSource.RootNodes.First(x => x.Name == "PM1"), out configNode));
            Assert.True(ConfigNode.FindByItemPath($"{configNode.Name}.Injet.EnableLogMessage", configNode, out var configItem1, out var configNode1));
        }
    }
}
