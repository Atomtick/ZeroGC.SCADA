using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using SCADA.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace SCADA.Configuration.Benchmarks
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        private readonly PrimitiveConfigSource _configSource;
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
        private readonly Consumer _consumer = new Consumer();

        public Benchmark()
        {
            Environment.CurrentDirectory = AppContext.BaseDirectory;
            _configSource = new PrimitiveConfigSource("configs.db");
            // 这里只需要执行一次就可以了
            iFAEnable = _configSource.SelectConfigItem("FA.Enable");
            iFAMode = _configSource.SelectConfigItem("FA.ConnectionMode");
            iT3Timeout = _configSource.SelectConfigItem("FA.T3Timeout");
            iNotchDegree = _configSource.SelectConfigItem("Aligner.NotchDegree");
            iStableCriteria = _configSource.SelectConfigItem("PM1.BiasRFMatch.StableCriteria");
            iStableTime = _configSource.SelectConfigItem("PM1.BiasRFMatch.StableTime");
            iAlarmRange = _configSource.SelectConfigItem("PM1.SourceRFPower.AlarmRange");
            iAlarmTime = _configSource.SelectConfigItem("PM1.SourceRFPower.AlarmTime");
            iWarningTime = _configSource.SelectConfigItem("PM1.SourceRFPower.WarningTime");
            iWarningRange = _configSource.SelectConfigItem("PM1.SourceRFPower.WarningRange");
        }

        [WarmupCount(5)]
        [Benchmark(Baseline = true/*, OperationsPerInvoke = 250000*/)] // 标记为基准测试，设为基准对比项
        public void HashSerachAndConvertStringToDouble()
        {
            //for (int i = 0; i < 250000; i++)
            //{
                double.Parse("3.14159");
                //_configSource.SelectConfigItem("FA.Enable");
            //}
        }

        [WarmupCount(5)]
        [Benchmark(/*OperationsPerInvoke = 250000*/)]
        public void ReadBatch()
        {
            //for (int i = 0; i < 250000; i++)
            //{
                (var vFAEnable, var vFAMode, var vT3Timeout, var vNotchDegree, var vStableCriteria, var vStableTime, var vAlarmRange, var vAlarmTime, var vWarningTime, var vWarningRange) =
                    _configSource.Read(iFAEnable, iFAMode, iT3Timeout, iNotchDegree, iStableCriteria, iStableTime, iAlarmRange, iAlarmTime, iWarningTime, iWarningRange);
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
                // 将值喂给 Consumer，防止 val 的读取被 JIT 删掉
                //_consumer.Consume(FAEnable);
                //_consumer.Consume(FAMode);
                //_consumer.Consume(T3Timeout);
                //_consumer.Consume(NotchDegree);
                //_consumer.Consume(TStableCriteria);
                //_consumer.Consume(stableTime);
                //_consumer.Consume(alarmRange);
                //_consumer.Consume(alarmtime);
                //_consumer.Consume(warningTime);
                //_consumer.Consume(warningrange);
            //}
        }

        [WarmupCount(5)]
        [Benchmark(/*OperationsPerInvoke = 250000*/)]
        public void ReadOne()
        {
            //for (int i = 0; i < 250000; i++)
            //{
                var vFAEnable = _configSource.Read(iFAEnable);
                var FAEnable = vFAEnable.ToBool();
                // 将值喂给 Consumer，防止 val 的读取被 JIT 删掉
                _consumer.Consume(FAEnable);
            //}
        }
    }
}
