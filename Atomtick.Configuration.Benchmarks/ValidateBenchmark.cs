using Atomtick.Configuration.Interfaces;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atomtick.Configuration.Benchmarks
{
    [MemoryDiagnoser(displayGenColumns: true)]
    [EventPipeProfiler(EventPipeProfile.GcVerbose)] // 会生成一个 .nettrace 文件，你可以用 Visual Studio 或 PerfView 打开，精确看到是哪一行代码触发了分配
    [GcServer(false)] // 强制使用 Workstation GC，降低内存阈值，更容易触发回收
    public class ValidateBenchmark
    {
        IConfigValidator _configSource;

        public ValidateBenchmark()
        {
            _configSource = new PrimitiveConfigSource("configs.db");
        }

        [WarmupCount(5)]
        [Benchmark()]
        public void ValidateValue_ValidBool()
        {
            _configSource.ValidateValue("FA.Enable", "true");
        }

        [WarmupCount(5)]
        [Benchmark()]
        public void ValidateValue_ValidInteger()
        {
            _configSource.ValidateValue("FA.LocalPortNumber", "1000");
        }

        [WarmupCount(5)]
        [Benchmark()]
        public void ValidateValue_ValidOptions()
        {
            _configSource.ValidateValue("TM.DryPump.DryPumpType", "EbaraS20P");
        }
    }
}
