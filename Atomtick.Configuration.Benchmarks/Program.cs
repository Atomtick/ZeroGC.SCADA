using Atomtick.Configuration.Benchmarks;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<Benchmark>();
var summary2 = BenchmarkRunner.Run<ValidateBenchmark>();
