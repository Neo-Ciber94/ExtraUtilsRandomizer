using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    [ShortRunJob]
    public class ModuloBenchmark
    {
        [Benchmark]
        public void NormalMod()
        {
            long N = 1000_000_000;
            long result = 0;
            for(long i = 0; i < N; i++)
            {
                result += Mod1(i, N - i);
            }
        }

        [Benchmark]
        public void ModFormula()
        {
            long N = 1000_000_000;
            long result = 0;
            for (long i = 0; i < N; i++)
            {
                result += Mod2(i, N - i);
            }
        }

        [Benchmark]
        public void FastMod()
        {
            long N = 1000_000_000;
            long result = 0;
            for (long i = 0; i < N; i++)
            {
                result += Mod3(i, N - i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long Mod1(long a, long b)
        {
            return a % b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long Mod2(long a, long b)
        {
            long temp = a / b;
            return a - (b * temp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long Mod3(long a, long b)
        {
            return a > b ? a % b : a;
        }
    }
}
