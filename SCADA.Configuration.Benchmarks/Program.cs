using BenchmarkDotNet.Running;
using SCADA.Configuration.Benchmarks;

var summary = BenchmarkRunner.Run<Benchmark>();
