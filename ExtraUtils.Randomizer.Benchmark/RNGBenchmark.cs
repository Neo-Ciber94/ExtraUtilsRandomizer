using BenchmarkDotNet.Attributes;
using System;

namespace ExtraUtils.Randomizer.Benchmark
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public class RNGBenchmark
    {
        [Benchmark]
        public void RNGNextChar()
        {
            var rng = RNG.Default;
            char c = rng.NextChar(RandomCharKind.Any);
        }

        [Benchmark(Baseline = true)]
        public void RandomNextChar()
        {
            var random = new Random();
            char c = (char)random.Next(33, 127);
        }
    }
}
