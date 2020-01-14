using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExtraUtils.Randomizer.Benchmark
{
    [ShortRunJob]
    [MinColumn, MaxColumn]
    public class NextBytesBenchmark
    {
        private const uint seed = 1234;

        [Params(10, 100, 1000, 10000, 100000)]
        public int BytesCount;

        [Benchmark]
        public void NextByteUnsafe()
        {
            Span<byte> span = stackalloc byte[BytesCount];
            NextBytes1(span);
        }

        [Benchmark]
        public void NextBytes()
        {
            Span<byte> span = stackalloc byte[BytesCount];
            NextBytes2(span);
        }

        private static void NextBytes1(Span<byte> destination)
        {
            var rng = new RNG(seed);
            int length = destination.Length;
            int i = 0;

            if (length > 4)
            {
                ref int cur = ref Unsafe.As<byte, int>(ref destination.GetPinnableReference());
                while (length > 4)
                {
                    Unsafe.AddByteOffset(ref cur, (IntPtr)i) = rng.NextInt();
                    length -= 4;
                    i += 4;
                }
            }

            for (; i < length; ++i)
            {
                destination[i] = (byte)(rng.NextInt() & byte.MaxValue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void NextBytes2(Span<byte> destination)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < destination.Length; ++i)
            {
                destination[i] = (byte)(rng.NextInt() & byte.MaxValue);
            }
        }
    }
}
