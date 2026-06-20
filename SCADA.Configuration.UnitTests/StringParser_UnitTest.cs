//using NUnit;
//using NUnit.Framework;

//namespace SCADA.Configuration.TestProject
//{
//    [TestFixture]
//    public class StringParser_UnitTest
//    {
//        [NUnit.Framework.Test]
//        public void IsValidColor_Test()
//        {
//            Assert.True(StringParser.TryParse2Color("#FFFFFF", out var _));
//            Assert.True(StringParser.TryParse2Color("#A0FFFFFF", out var _));
//            Assert.True(StringParser.TryParse2Color("#ffffff", out var _));
//            Assert.True(StringParser.TryParse2Color("#A0ffffff", out var _));
//            Assert.True(StringParser.TryParse2Color("#123456", out var _));
//            Assert.False(StringParser.TryParse2Color("#FFFFF", out var _));
//            Assert.False(StringParser.TryParse2Color("#fffff", out var _));
//            Assert.False(StringParser.TryParse2Color("A0FFFFFF", out var _));
//            Assert.False(StringParser.TryParse2Color("#GHIJKL", out var _));
//            Assert.False(StringParser.TryParse2Color("#12345G", out var _));
//            Assert.False(StringParser.TryParse2Color("123456", out var _));
//        }

//        [NUnit.Framework.Test]
//        public void IsValidDateTime_Test()
//        {
//            Assert.True(StringParser.TryParse2DateTime("2026-04-10 00:20:00", out var _));
//            Assert.False(StringParser.TryParse2DateTime("20260410002000", out var _));
//            Assert.False(StringParser.TryParse2DateTime("2026/4/10", out var _));
//            Assert.False(StringParser.TryParse2DateTime("2026.4.10", out var _));
//        }

//        [NUnit.Framework.Test]
//        public void IsValidFile_Test()
//        {
//            Assert.True(StringParser.TryParse2File("C:\\Test\\test.txt", out var _));
//            Assert.True(StringParser.TryParse2File("D:\\Test\\test.txt", out var _));
//            Assert.True(StringParser.TryParse2File("Z:\\Test\\test.txt\\..", out var _));
//            Assert.True(StringParser.TryParse2File("C:\\Test\\..\\test.txt", out var _));
//            Assert.True(StringParser.TryParse2File("C:\\Test\\test.txt\\#", out var _));
//            Assert.False(StringParser.TryParse2File("D:\\Test\\\\test.txt", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\test.txt\\?", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\test*?.txt", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\test.txt\\", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\\\test.txt", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\test.txt/", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\test.txt?", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\Test\\test.txt*", out var _));
//            Assert.False(StringParser.TryParse2File("C:\\", out var _));
//            Assert.False(StringParser.TryParse2File("C:", out var _));
//            Assert.False(StringParser.TryParse2File("C", out var _));
//        }

//        [NUnit.Framework.Test]
//        public void IsValidFolder_Test()
//        {
//            Assert.True(StringParser.TryParse2Directory("C:\\Test\\", out var _));
//            Assert.True(StringParser.TryParse2Directory("D:\\Test\\", out var _));
//            Assert.True(StringParser.TryParse2Directory("Z:\\Test\\..\\", out var _));
//            Assert.True(StringParser.TryParse2Directory("C:\\Test\\..\\", out var _));
//            Assert.True(StringParser.TryParse2Directory("C:\\Test\\#", out var _));
//            Assert.True(StringParser.TryParse2Directory("C:\\", out var _));
//            Assert.True(StringParser.TryParse2Directory("C:\\Test", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:", out var _));
//            Assert.False(StringParser.TryParse2Directory("D:\\\\", out var _));
//            Assert.False(StringParser.TryParse2Directory("D:\\\\Test\\", out var _));
//            Assert.False(StringParser.TryParse2Directory("B:\\", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:\\Test\\?", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:\\Test\\*", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:\\Test\\test.txt/", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:\\Test\\\\", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:\\Test\\test.txt?", out var _));
//            Assert.False(StringParser.TryParse2Directory("C:\\Test\\test.txt*", out var _));
//        }
//    }
//}