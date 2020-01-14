using BenchmarkDotNet.Running;
using ExtraUtils.Randomizer;
using ExtraUtils.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    class Program
    {
        static void Main()
        {
            //for (long i = 0; i <= 1000; i++)
            //{
            //    Console.WriteLine(RNG.Default.NextULong());
            //}
        }
    }

    public static class Utils
    {
        static string SpanToString<T>(Span<T> span)
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < span.Length; i++)
            {
                sb.Append(span[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }

        static bool InRange<T>(T value, T min, T max) where T : IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
    }
}
