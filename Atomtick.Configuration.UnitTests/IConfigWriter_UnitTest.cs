using System;
using System.Collections.Generic;
using System.Text;
using Atomtick.Configuration;

namespace SCADA.Configuration.UnitTests
{
    public class IConfigWriter_UnitTest
    {
        [Fact]
        public void Test()
        {
            var settings = new ConfigSettings { IsConfigModificationTrackingEnabled = true };

            // 写入
            var configSource = new PrimitiveConfigSource("configs.db", settings);

            bool enable = false;
            for (var i = 0; i < 1000; i++)
            {
                configSource
                    .BeginTransaction(out long transactionId)
                    .Write(transactionId, "FA.IsEnabled", (!enable).ToString())
                    .Write(transactionId, "FA.LocalIpAddress", $"192.168.0.{++i}")
                    .Write(transactionId, "FA.LocalPortNumber", (++i).ToString())
                    .Write(transactionId, "FA.T3Timeout", (++i).ToString())
                    .Write(transactionId, "FA.LogPath", "C:\\Logs")
                    .CommitTransaction(transactionId);
            }

            configSource.Dispose();

            // 读取
            configSource = new PrimitiveConfigSource("configs.db", settings);
            var _enable = configSource.Select("FA.IsEnabled");
            var _ip = configSource.Select("FA.LocalIpAddress");
            var _port = configSource.Select("FA.LocalPortNumber");
            var _t3Timeout = configSource.Select("FA.T3Timeout");
            var _logPath = configSource.Select("FA.LogPath");
            var configs = configSource.Read(_enable, _ip, _port, _t3Timeout, _logPath);

            Assert.True(configs.Item1.ToBool());
            Assert.Equal("192.168.0.997", configs.Item2.ToString());
            Assert.Equal(998, configs.Item3.ToInt32());
            Assert.Equal(999, configs.Item4.ToDouble());
            Assert.Equal("C:\\Logs", configs.Item5.ToDirectory().FullName);
        }
    }
}
