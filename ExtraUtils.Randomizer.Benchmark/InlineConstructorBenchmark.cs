using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;

namespace ExtraUtils.Randomizer.Benchmark
{
    [MinColumn, MaxColumn]
    public class InlineConstructorBenchmark
    {
        struct Sample1
        {
            private int value;

            public Sample1(int value)
            {
                this.value = value;
            }
        }

        struct Sample2
        {
            private int value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Sample2(int value)
            {
                this.value = value;
            }
        }

        [Benchmark]
        public void CallConstructor()
        {
            var s = new Sample1(10);
        }

        [Benchmark]
        public void CallInlineConstructor()
        {
            var s = new Sample2(10);
        }
    }
}
