using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SCADA.Configuration.TestProject
{
    public class Build_Construct_UnitTest
    {
        [Fact]
        public void Load()
        {
            using (PrimitiveConfigSource source = new PrimitiveConfigSource("system.xml"))
            {

            }
        }
    }
}
