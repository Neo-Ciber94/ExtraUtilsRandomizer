using BenchmarkDotNet.Running;
using ExtraUtils.Randomizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    class Program
    {
        static void Main()
        {
            //Span<byte> span = stackalloc byte[8];
            //for (int i = 0; i < 1000_000_000; i++)
            //{
            //    Console.WriteLine(RNG.Default.NextInt(int.MinValue, int.MaxValue - 20));
            //}

            for (long i = 0; i <=1000; i++)
            {
                if (PrimeNumberHelper.IsPrime(i))
                {
                    Console.WriteLine(i);
                }
            }

            //RunBenchmarks();
        }

        static string SpanToString<T>(Span<T> span)
        {
            StringBuilder sb = new StringBuilder("[");
            for(int i = 0; i < span.Length; i++)
            {
                sb.Append(span[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }

        static bool InRange<T>(T value, T min, T max) where T: IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        static IEnumerable<long> PrimeNumberEnumerator(uint n)
        {
            while (true)
            {
                if (PrimeNumberHelper.IsPrime(n))
                {
                    yield return n;
                }

                ++n;
            }
        }

        static void RunBenchmarks()
        {
            //BenchmarkRunner.Run<RNGBenchmark>();
            BenchmarkRunner.Run<ModuloBenchmark>();
        }
    }
}
