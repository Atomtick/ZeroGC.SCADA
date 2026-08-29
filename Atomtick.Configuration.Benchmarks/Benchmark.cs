using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;

namespace Atomtick.Configuration.Benchmarks
{
    [MemoryDiagnoser(displayGenColumns: true)]
    [EventPipeProfiler(EventPipeProfile.GcVerbose)] // 会生成一个 .nettrace 文件，你可以用 Visual Studio 或 PerfView 打开，精确看到是哪一行代码触发了分配
    [GcServer(false)] // 强制使用 Workstation GC，降低内存阈值，更容易触发回收
    public class Benchmark
    {
        private readonly PrimitiveConfigSource _configSource;
        private readonly PrimitiveConfigSource _configSource2;
        private long _i;
        private readonly ConfigItem iFAEnable;
        private readonly ConfigItem iFAMode;
        private readonly ConfigItem iT3Timeout;
        private readonly ConfigItem iNotchDegree;
        private readonly ConfigItem iStableCriteria;
        private readonly ConfigItem iStableTime;
        private readonly ConfigItem iAlarmRange;
        private readonly ConfigItem iAlarmTime;
        private readonly ConfigItem iWarningTime;
        private readonly ConfigItem iWarningRange;
        private readonly ConfigItem iEnableLogMessage;
        private readonly ConfigItem iBaudRate;
        private readonly ConfigItem iDataBits;
        private readonly ConfigItem iWindowHeater;
        private readonly ConfigItem iPumpingLine;
        private readonly ConfigItem iBaratron;
        private readonly Consumer _consumer = new Consumer();

        public Benchmark()
        {
            Environment.CurrentDirectory = AppContext.BaseDirectory;
            _configSource = new PrimitiveConfigSource("configs.db");
            _configSource2 = new PrimitiveConfigSource("configs.db");
            // 这里只需要执行一次就可以了
            iFAEnable = _configSource.Select("FA.Enable");
            iFAMode = _configSource.Select("FA.ConnectionMode");
            iT3Timeout = _configSource.Select("FA.T3Timeout");
            iNotchDegree = _configSource.Select("Aligner.NotchDegree");
            iStableCriteria = _configSource.Select("PM1.BiasRFMatch.StableCriteria");
            iStableTime = _configSource.Select("PM1.BiasRFMatch.StableTime");
            iAlarmRange = _configSource.Select("PM1.SourceRFPower.AlarmRange");
            iAlarmTime = _configSource.Select("PM1.SourceRFPower.AlarmTime");
            iWarningTime = _configSource.Select("PM1.SourceRFPower.WarningTime");
            iWarningRange = _configSource.Select("PM1.SourceRFPower.WarningRange");
            iEnableLogMessage = _configSource.Select("PM1.DryPump.Edwards.EnableLogMessage");
            iBaudRate = _configSource.Select("PM1.HeaterController.BaudRate");
            iDataBits = _configSource.Select("PM1.HeaterController.DataBits");
            iWindowHeater = _configSource.Select("PM1.WindowHeater.IsInstalled");
            iPumpingLine = _configSource.Select("PM1.PumpingLineHeater.IsInstalled");
            iBaratron = _configSource.Select("PM1.BaratronLineHeater.IsInstalled");
        }

        [WarmupCount(5)]
        [Benchmark(Baseline = true)] // 标记为基准测试，设为基准对比项
        public void ParseStringToDouble()
        {
            double.Parse("3.14159");
        }

        //[WarmupCount(5)]
        //[Benchmark()]
        public void HashSearch()
        {
            _configSource.Select("PM1.BaratronLineHeater.IsInstalled");
        }

        //[WarmupCount(5)]
        //[Benchmark()]
        public void Read16Items()
        {
            (
                var vFAEnable,
                var vFAMode,
                var vT3Timeout,
                var vNotchDegree,
                var vStableCriteria,
                var vStableTime,
                var vAlarmRange,
                var vAlarmTime,
                var vWarningTime,
                var vWarningRange,
                var vEnableLog,
                var vBaudRate,
                var vDataBits,
                var vWindowHeater,
                var vPumpingLine,
                var vBaratron
            ) = _configSource.Read(
                iFAEnable,
                iFAMode,
                iT3Timeout,
                iNotchDegree,
                iStableCriteria,
                iStableTime,
                iAlarmRange,
                iAlarmTime,
                iWarningTime,
                iWarningRange,
                iEnableLogMessage,
                iBaudRate,
                iDataBits,
                iWindowHeater,
                iPumpingLine,
                iBaratron
            );
            var FAEnable = vFAEnable.ToBool();
            var FAMode = vFAMode.ToString();
            var T3Timeout = vT3Timeout.ToInt32();
            var NotchDegree = vNotchDegree.ToDouble();
            var TStableCriteria = vStableCriteria.ToDouble();
            var stableTime = vStableTime.ToDouble();
            var alarmRange = vAlarmRange.ToDouble();
            var alarmtime = vAlarmTime.ToDouble();
            var warningTime = vWarningTime.ToDouble();
            var warningrange = vWarningRange.ToDouble();
            var EnableLogMessage = vEnableLog.ToBool();
            var BaudRate = vBaudRate.ToInt32();
            var DataBits = vDataBits.ToInt32();
            var WindowHeater = vWindowHeater.ToBool();
            var PumpingLine = vPumpingLine.ToBool();
            var Baratron = vBaratron.ToBool();
            // 将值喂给 Consumer，防止 val 的读取被 JIT 删掉
            _consumer.Consume(FAEnable);
            _consumer.Consume(FAMode);
            _consumer.Consume(T3Timeout);
            _consumer.Consume(NotchDegree);
            _consumer.Consume(TStableCriteria);
            _consumer.Consume(stableTime);
            _consumer.Consume(alarmRange);
            _consumer.Consume(alarmtime);
            _consumer.Consume(warningTime);
            _consumer.Consume(warningrange);
            _consumer.Consume(EnableLogMessage);
            _consumer.Consume(BaudRate);
            _consumer.Consume(DataBits);
            _consumer.Consume(WindowHeater);
            _consumer.Consume(PumpingLine);
            _consumer.Consume(Baratron);
        }

        //[WarmupCount(5)]
        //[Benchmark()]
        public void ReadOneItem()
        {
            var vFAEnable = _configSource.Read(iFAEnable);
            var FAEnable = vFAEnable.ToBool();
            // 将值喂给 Consumer，防止 val 的读取被 JIT 删掉
            _consumer.Consume(FAEnable);
        }

        private bool _FaEnable = true;
        [WarmupCount(5)]
        [Benchmark()]
        public void Write10Items()
        {
            _configSource2
                .BeginTransaction(out long transactionId)
                .Write(transactionId, "FA.Enable", (!_FaEnable).ToString())
                .Write(transactionId, "FA.LocalIpAddress", $"192.168.{_i++}.1")
                .Write(transactionId, "FA.LocalPortNumber", (_i++).ToString())
                .Write(transactionId, "FA.T3Timeout", (_i++).ToString())
                .Write(transactionId, "FA.T5Timeout", (_i++).ToString())
                .Write(transactionId, "FA.T6Timeout", (_i++).ToString())
                .Write(transactionId, "FA.T7Timeout", (_i++).ToString())
                .Write(transactionId, "FA.T8Timeout", (_i++).ToString())
                .Write(transactionId, "FA.LinkTestInterval", (_i++).ToString())
                .Write(transactionId, "FA.initial_valueControlSubState", _FaEnable ? "Local" : "Remote")
                .CommitTransaction(transactionId);
            _configSource2.Dispose();
        }
    }
}
