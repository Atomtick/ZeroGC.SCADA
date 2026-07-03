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
            var configSource = new PrimitiveConfigSource("configs.db");

            // 这里只需要执行一次就可以了
            var iFAEnable = configSource.SelectConfigItem("FA.Enable");
            var iFAMode = configSource.SelectConfigItem("FA.ConnectionMode");
            var iT3Timeout = configSource.SelectConfigItem("FA.T3Timeout");
            var iNotchDegree = configSource.SelectConfigItem("Aligner.NotchDegree");
            var iRecipePath = configSource.SelectConfigItem("System.RecipePath");

            // 第一次原子性的批量获取多个配置项的值
            (var vFAEnable, var vFAMode, var vT3Timeout, var vNotchDegree, var vRecipePath) = configSource.Read(iFAEnable, iFAMode, iT3Timeout, iNotchDegree, iRecipePath);
            var FAEnable = vFAEnable.ToBool();
            var FAMode = vFAMode.ToString();
            var T3Timeout = vT3Timeout.ToInt32();
            var NotchDegree = vNotchDegree.ToDouble();
            var RecipePath = vRecipePath.ToDirectory();
            // 第二次原子性的批量获取多个配置项的值(如果配置值有变化,第二次读取的最新值显然会和第一次不一样)
            (vFAEnable, vFAMode, vT3Timeout, vNotchDegree, vRecipePath) = configSource.Read(iFAEnable, iFAMode, iT3Timeout, iNotchDegree, iRecipePath);
            FAEnable = vFAEnable.ToBool();
            FAMode = vFAMode.ToString();
            T3Timeout = vT3Timeout.ToInt32();
            NotchDegree = vNotchDegree.ToDouble();
            RecipePath = vRecipePath.ToDirectory();
        }

        //[Fact]
        //public void Test()
        //{
        //    var primitiveConfigSource = new PrimitiveConfigSource("D:\\CodeRepo\\ZeroGC.SCADA\\SCADA.Configuration\\configs.db");

        //    var IsSimulatorMode = primitiveConfigSource.SelectConfigItem("System.IsSimulatorMode");

        //    var isSimulatorModeValue = primitiveConfigSource.Read(IsSimulatorMode);
        //    var result = isSimulatorModeValue.ToBool();

        //    var VentBasePressure = primitiveConfigSource.SelectConfigItem("VCE.Vent.VentBasePressure");
        //    var VentBasePressureValue = primitiveConfigSource.Read(VentBasePressure);
        //    var result2 = VentBasePressureValue.ToDouble();

        //    primitiveConfigSource
        //        .BeginTransaction(out long transactionId)
        //        .Write(transactionId, "System.IP", "192.168.0.1")
        //        .Write(transactionId, "System.Port", 5432)
        //        .Write(transactionId, "System.Enabled", true)
        //        .Write(transactionId, "System.AlarmColor", System.Drawing.Color.Red)
        //        .Write(transactionId, "System.StartTime", new DateTime(2026, 4, 10, 0, 20, 0))
        //        .Write(transactionId, "System.LogPath", "C:\\Logs")
        //        .Write(transactionId, "System.UserInfo", "D:\\UserInfo.json")
        //        .CommitTransaction(transactionId);
        //}

        public class TransferModule
        {
            private readonly IConfigReader _configSource;
            private readonly ConfigItem _iHomeTimeout;
            private readonly ConfigItem _iMoveDistanceAfterHome;
            private readonly string _robotIP;
            private readonly int _bladeSlots;

            public TransferModule(IConfigReader configReader)
            {
                _configSource = configReader;
                _iHomeTimeout = configReader.SelectConfigItem("TM.HomeTimeout");

                var iBladeSlotes = configReader.SelectConfigItem("TM.BladeSlots");
                var iRobotIp = configReader.SelectConfigItem("TM.RobotIP");
                (var _bladeSlotsValue, var _robotIpValue) = configReader.Read(iBladeSlotes, iRobotIp);
                _bladeSlots = _bladeSlotsValue.ToInt32();
                _robotIP = _robotIpValue.ToString();

                var iIsSimulatorMode = _configSource.SelectConfigItem("System.IsSimulatorMode");
                var iCycleCount = _configSource.SelectConfigItem("System.CycleCount");

                (var vSimulatorMode, var vCycleCount) = _configSource.Read(iIsSimulatorMode, iCycleCount);

                var simulatorMode = vSimulatorMode.ToBool(true);
                var cycleCount = vCycleCount.ToInt32();
            }

            public void Home()
            {
                var configValues = _configSource.Read(_iHomeTimeout, _iMoveDistanceAfterHome);
                int homeTimeout = configValues.Item1.ToInt32(30);
                int moveDistanceAfterHome = configValues.Item2.ToInt32(15);
            }
        }
    }
}
