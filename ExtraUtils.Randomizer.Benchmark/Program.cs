using BenchmarkDotNet.Running;
using ExtraUtils.Randomizer;
using ExtraUtils.Randomizer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    class Program
    {
        static void Main()
        {
            var rng = new RNG(123);

            Span<char> span = stackalloc char[10];

            rng.NextString(span);

            Console.WriteLine(span.ToString());
        }
    }

    public static class Utils
    {
        public static string SpanToString<T>(Span<T> span)
        {
            StringBuilder sb = new StringBuilder("[");
            var enumerator = span.GetEnumerator();

            if(enumerator.MoveNext())
            {
                while (true)
                {
                    ref T value = ref enumerator.Current;
                    sb.Append(value.ToString());

                    if (enumerator.MoveNext())
                    {
                        sb.Append(", ");
                    }
                    else
                    {
                        break;
                    }
                }
            }

            sb.Append("]");
            return sb.ToString();
        }

        public static bool InRange<T>(T value, T min, T max) where T : IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
    }
}
