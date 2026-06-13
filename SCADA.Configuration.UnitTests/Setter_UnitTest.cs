using System;
using System.Collections.Generic;
using System.Text;
using SCADA.Common.Interfaces;

namespace SCADA.Configuration.UnitTests
{
    public class Setter_UnitTest
    {
        //private PrimitiveConfigSource primitiveConfigSource =
        //    new PrimitiveConfigSource("C:\\Users\\Admin\\Desktop\\1.xml",false,1000);

        [Fact]
        public void Test()
        {
            PrimitiveConfigSource primitiveConfigSource = new PrimitiveConfigSource(
                "D:\\CodeRepo\\ZeroGC.SCADA\\SCADA.Configuration\\configs.db"
            );


            var IsSimulatorMode = primitiveConfigSource.SelectConfigItem("System.IsSimulatorMode");
            primitiveConfigSource.Read(IsSimulatorMode, out IConfigValue isSimulatorModeValue);
            var result = isSimulatorModeValue.ToBool();

            var VentBasePressure = primitiveConfigSource.SelectConfigItem("VCE.Vent.VentBasePressure");
            primitiveConfigSource.Read(VentBasePressure, out IConfigValue VentBasePressureValue);
            var result2 = VentBasePressureValue.ToDouble();



            primitiveConfigSource
                .BeginTransaction(out long transactionId)
                .Write(transactionId, "System.IP", "192.168.0.1")
                .Write(transactionId, "System.Port", 5432)
                .Write(transactionId, "System.Enabled", true)
                .Write(transactionId, "System.AlarmColor", System.Drawing.Color.Red)
                .Write(transactionId, "System.StartTime", new DateTime(2026, 4, 10, 0, 20, 0))
                .Write(transactionId, "System.LogPath", "C:\\Logs")
                .Write(transactionId, "System.UserInfo", "D:\\UserInfo.json")
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
