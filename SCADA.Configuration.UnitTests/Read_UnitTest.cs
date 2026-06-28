using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SCADA.Configuration.Interfaces;

namespace SCADA.Configuration.UnitTests
{
    public class Read_UnitTest
    {
        [Fact]
        public void Test()
        {
            var primitiveConfigSource = new PrimitiveConfigSource("D:\\CodeRepo\\ZeroGC.SCADA\\SCADA.Configuration\\configs.db");

            var IsSimulatorMode = primitiveConfigSource.SelectConfigItem("System.IsSimulatorMode");

            var isSimulatorModeValue = primitiveConfigSource.Read(IsSimulatorMode);
            var result = isSimulatorModeValue.ToBool();

            var VentBasePressure = primitiveConfigSource.SelectConfigItem("VCE.Vent.VentBasePressure");
            var VentBasePressureValue = primitiveConfigSource.Read(VentBasePressure);
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

        public class TransferModule
        {
            private readonly IConfigReader _configSource;
            private readonly ConfigItem _homeTimeoutItem;
            private readonly ConfigItem _moveDistanceAfterHomeItem;
            private readonly string _robotIP;
            private readonly int _bladeSlots;

            public TransferModule(IConfigReader configReader)
            {
                _configSource = configReader;
                _homeTimeoutItem = configReader.SelectConfigItem("TM.HomeTimeout");

                var bladeSlotesItem = configReader.SelectConfigItem("TM.BladeSlots");
                var robotIpItem = configReader.SelectConfigItem("TM.RobotIP");
                (var _bladeSlotsValue, var _robotIPValue) = configReader.Read(bladeSlotesItem, robotIpItem);
                _bladeSlots = _bladeSlotsValue.ToInt32();
                _robotIP = _robotIPValue.ToString();
            }

            public void Home()
            {
                var configValues = _configSource.Read(_homeTimeoutItem, _moveDistanceAfterHomeItem);
                int homeTimeout = configValues.Item1.ToInt32(30);
                int moveDistanceAfterHome = configValues.Item2.ToInt32(15);
            }
        }
    }
}
