using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    [ShortRunJob]
    [MinColumn, MaxColumn]
    public class MethodInlineBenchmark
    {
        [Benchmark]
        public void NoInlined()
        {
            int result = Sum1(10, 4);
        }

        [Benchmark]
        public void Inlined()
        {
            int result = Sum2(10, 4);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int Sum1(int a, int b)
        {
            return a + b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Sum2(int a, int b)
        {
            return a + b;
        }
    }
}
