using NUnit;
using NUnit.Framework;
using SCADA.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCADA.Configuration.UnitTests
{
    [TestFixture]
    public class Setter_UnitTest
    {
        //private PrimitiveConfigSource primitiveConfigSource =
        //    new PrimitiveConfigSource("C:\\Users\\Admin\\Desktop\\1.xml",false,1000);

        [Test]
        public void Test()
        {
            PrimitiveConfigSource primitiveConfigSource =
           new PrimitiveConfigSource("C:\\Users\\Admin\\Desktop\\1.xml", false, 1000);

            primitiveConfigSource.BeginTransaction(out long transactionId)
                .Set(transactionId, "System.IP", "192.168.0.1")
                .Set(transactionId, "System.Port", 5432)
                .Set(transactionId, "System.Enabled", true)
                .Set(transactionId, "System.AlarmColor", System.Drawing.Color.Red)
                .Set(transactionId, "System.StartTime", new DateTime(2026, 4, 10, 0, 20, 0))
                .Set(transactionId, "System.LogPath", "C:\\Logs")
                .Set(transactionId, "System.UserInfo", "D:\\UserInfo.json")
                .CommitTransaction(transactionId);
        }

        //public class Device
        //{
        //    private IConfigSourceReader _configSource;

        //    public Device (IConfigSourceReader configSource)
        //    {
        //        _configSource = configSource;
        //        _alarmTolerance = _configSource.Get<IIntegerConfigItem>("Device.AlarmTolerance");
        //    }

        //    private IIntegerConfigItem _alarmTolerance;

        //    public void CheckAlarm(double value)
        //    {
        //        _configSource.Get(_alarmTolerance, out object obj);

        //        obj.ToInt32(_alarmTolerance);

        //        if (value > alarmTolerance)
        //        {
        //            Console.WriteLine($"Alarm! Value {value} exceeds tolerance {alarmTolerance}");
        //        }
        //    }
        //}
    }
}