using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Atomtick.Configuration;
using Atomtick.Configuration.Interfaces;

namespace SCADA.Configuration.UnitTests
{
    public class Read_UnitTest
    {
        [Fact]
        public void Test()
        {
            var configSource = new PrimitiveConfigSource("configs.db");

            // 这里只需要执行一次就可以了
            var FAEnable_i = configSource.Select("FA.Enable");
            var FAMode_i = configSource.Select("FA.ConnectionMode");
            var t3Timeout_i = configSource.Select("FA.T3Timeout");
            var notchDegree_i = configSource.Select("Aligner.NotchDegree");
            var recipePath_i = configSource.Select("System.RecipePath");

            // 第一次原子性的批量获取多个配置项的值
            (var FAEnable_v, var FAMode_v, var T3Timeout_v, var notchDegree_v, var recipePath_v) = configSource.Read(FAEnable_i, FAMode_i, t3Timeout_i, notchDegree_i, recipePath_i);
            var FAEnable = FAEnable_v.ToBool();
            var FAMode = FAMode_v.ToString();
            var T3Timeout = T3Timeout_v.ToInt32();
            var NotchDegree = notchDegree_v.ToDouble();
            var RecipePath = recipePath_v.ToDirectory();
            // 第二次原子性的批量获取多个配置项的值(如果配置值有变化,第二次读取的最新值显然会和第一次不一样)
            (FAEnable_v, FAMode_v, T3Timeout_v, notchDegree_v, recipePath_v) = configSource.Read(FAEnable_i, FAMode_i, t3Timeout_i, notchDegree_i, recipePath_i);
            FAEnable = FAEnable_v.ToBool();
            FAMode = FAMode_v.ToString();
            T3Timeout = T3Timeout_v.ToInt8();
            NotchDegree = notchDegree_v.ToDouble();
            RecipePath = recipePath_v.ToDirectory();
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
            private readonly IConfigReader _configReader;
            private readonly ConfigItem _homeTimeout_i;
            private readonly ConfigItem _maxPressureDiffOpenSlitValve_i;
            private readonly ConfigItem _atmPressureBase_i;
            private readonly ConfigItem _vacuumPressureBase_i;
            private readonly ConfigItem _robotIp_i;
            private readonly ConfigItem _robotPort_i;

            public TransferModule(IConfigReader configReader)
            {
                _configReader = configReader;
                _homeTimeout_i = _configReader.Select("TM.HomeTimeout");
                _maxPressureDiffOpenSlitValve_i = _configReader.Select("TM.MaxPressureDiffOpenSlitValve");
                _atmPressureBase_i = _configReader.Select("TM.AtmPressureBase");
                _vacuumPressureBase_i = _configReader.Select("TM.VacuumPressureBase");
                _robotIp_i = _configReader.Select("TM.RobotIP");
                _robotPort_i = _configReader.Select("TM.RobotPort");
            }

            public void Init()
            {

                (var ip_v, var port_v) = _configReader.Read(_robotIp_i, _robotPort_i);
                var ip = ip_v.ToString();
                var port = port_v.ToInt32();
                Connect(ip, port);
            }

            public void Home()
            {
                var configValues = _configReader.Read(_homeTimeout_i, _atmPressureBase_i, _vacuumPressureBase_i, _maxPressureDiffOpenSlitValve_i);
                int homeTimeout = configValues.Item1.ToInt32();
                var atmPressureBase = configValues.Item2.ToDouble();
                var vacuumPressureBase = configValues.Item3.ToDouble();
                var maxPressureDiffOpenSlitValve = configValues.Item4.ToDouble();
            }

            private void Connect(string ip, int port)
            {
                // ......
            }
        }
    }
}
