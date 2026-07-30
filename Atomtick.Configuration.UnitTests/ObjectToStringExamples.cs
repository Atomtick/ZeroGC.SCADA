using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Configuration.UnitTests
{
    public class ObjectToStringExamples
    {
        [Fact]
        public void Test()
        {
            // double
            Assert.Equal("0123.15", 123.14526.ToString("0000.00"));
            Assert.Equal("123.15", 123.14526.ToString(".00"));
            Assert.Equal("123.145", 123.14526.ToString(".000"));

            // color
            System.Drawing.Color color = System.Drawing.Color.FromArgb(255, 255, 10, 10);
            Assert.Equal("#FFFF0A0A", $"#{color.ToArgb():X8}");
            Assert.Equal("#FF0A0A", $"#{color.ToArgb() & 0x00FFFFFF:X6}");

            // DateTime
            DateTime dt = new DateTime(2024, 6, 1, 12, 30, 45, 869);
            Assert.Equal("2024-06-01 12:30:45.869", dt.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            Assert.Equal("2024-06-01", dt.ToString("yyyy-MM-dd"));
            Assert.Equal("2024年6月1日", dt.ToString("yyyy年M月d日"));


        }
    }
}
