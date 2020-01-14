using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    [MinColumn, MaxColumn]
    public class NextLongBenchmark
    {
        private int Iterations = 1000;

        [Benchmark]
        public void NextLongUnsafe()
        {
            for(int i = 0; i < Iterations; i++)
            {
                long n = NextLongUnsafeImpl();
            }
        }

        [Benchmark]
        public void NextLong()
        {
            for (int i = 0; i < Iterations; i++)
            {
                long n = NextLongImpl();
            }
        }

        private static long NextLongUnsafeImpl()
        {
            var rng = RNG.Default;
            unsafe
            {
                byte* buffer = stackalloc byte[8];
                for (int i = 0; i < 8; i++)
                {
                    buffer[i] = (byte)(rng.NextInt() & byte.MaxValue);
                }

                return *(long*)buffer;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long NextLongImpl()
        {
            var rng = RNG.Default;
            return rng.NextInt() + (long)(rng.NextInt() << 32);
        }
    }
}
