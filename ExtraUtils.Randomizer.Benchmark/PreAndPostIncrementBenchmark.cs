using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    public class PreAndPostIncrementBenchmark
    {
        [Params(10, 100, 1000, 10_000, 100_000, 1000_000)]
        public int Length;

        [Benchmark]
        public void PostIncrement()
        {
            for(int i = 0; i < Length; i++)
            {
                var value = i;
            }
        }

        [Benchmark]
        public void PreIncrement()
        {
            for (int i = 0; i < Length; ++i)
            {
                var value = i;
            }
        }
    }
}
