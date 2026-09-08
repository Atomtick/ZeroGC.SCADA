using Atomtick.Configuration.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark_ReadWrite>();
BenchmarkRunner.Run<Benchmark_Validate>();
