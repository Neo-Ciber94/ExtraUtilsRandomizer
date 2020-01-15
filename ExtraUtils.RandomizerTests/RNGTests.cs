using Xunit;
using ExtraUtils.Randomizer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ExtraUtils.Randomizer.Tests
{
    public class RNGTests
    {
        private const int Iterations = 1000_000;

        [Theory()]
        [InlineData(123)]
        [InlineData(456)]
        [InlineData(789)]
        public void NextIntTest(int seed)
        {
            var rng = new RNG(seed);

            for(int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextInt(), int.MinValue, int.MaxValue);
            }
        }

        [Theory()]
        [InlineData(123, 100)]
        [InlineData(456, 10000)]
        [InlineData(789, 100000)]
        public void NextIntTest1(int seed, int max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextInt(max), 0, max);
            }
        }

        [Theory()]
        [InlineData(123, 10, 100)]
        [InlineData(456, 1000, 100000)]
        [InlineData(789, 10000, 1000000)]
        public void NextIntTest2(int seed, int min, int max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextInt(min, max), min, max);
            }
        }

        [Theory()]
        [InlineData(123)]
        [InlineData(456)]
        [InlineData(789)]
        public void NextLongTest(int seed)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextLong(), long.MinValue, long.MaxValue);
            }
        }

        [Theory()]
        [InlineData(123, 100)]
        [InlineData(456, 1000_000_000)]
        [InlineData(789, 3000_000_000_000)]
        public void NextLongTest1(int seed, long max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextLong(max), 0, max);
            }
        }

        [Theory()]
        [InlineData(123, 10, 100)]
        [InlineData(456, 5000_000_000, 6000_000_000)]
        [InlineData(789, 7000_000_000, 10_000_000_000_000)]
        public void NextLongTest2(int seed, long min, long max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextLong(min, max), min, max);
            }
        }

        [Theory()]
        [InlineData(123)]
        [InlineData(456)]
        [InlineData(789)]
        public void NextUIntTest(int seed)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextUInt(), uint.MinValue, uint.MaxValue);
            }
        }

        [Theory()]
        [InlineData(123, 100)]
        [InlineData(456, 10000)]
        [InlineData(789, 100000)]
        public void NextUIntTest1(int seed, uint max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextUInt(max), 0u, max);
            }
        }

        [Theory()]
        [InlineData(123, 10, 100)]
        [InlineData(456, 1000, 100000)]
        [InlineData(789, 10000, 1000000)]
        public void NextUIntTest2(int seed, uint min, uint max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextUInt(min, max), min, max);
            }
        }

        [Theory()]
        [InlineData(123)]
        [InlineData(456)]
        [InlineData(789)]
        public void NextULongTest(int seed)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextULong(), ulong.MinValue, ulong.MaxValue);
            }
        }

        [Theory()]
        [InlineData(123, 100)]
        [InlineData(456, 1000_000_000)]
        [InlineData(789, 3000_000_000_000)]
        public void NextULongTest1(int seed, ulong max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextULong(max), 0uL, max);
            }
        }

        [Theory()]
        [InlineData(123, 10, 100)]
        [InlineData(456, 5000_000_000, 6000_000_000)]
        [InlineData(789, 7000_000_000, 10_000_000_000_000)]
        public void NextULongTest2(int seed, ulong min, ulong max)
        {
            var rng = new RNG(seed);

            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextULong(min, max), min, max);
            }
        }


        [Fact()]
        public void NextDoubleTest()
        {
            var rng = RNG.Default;
            for(int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextDouble(), 0.0, 1.0);
            }
        }

        [Theory()]
        [InlineData(1000.0)]
        [InlineData(5000.0)]
        [InlineData(7000000000.0)]
        public void NextDoubleTest1(double max)
        {
            var rng = RNG.Default;
            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextDouble(max), 0.0, max);
            }
        }

        [Theory()]
        [InlineData(1000.0, 3000.0)]
        [InlineData(5000.0, 500000.0)]
        [InlineData(7000000000.0, 9000000000.0)]
        public void NextDoubleTest2(double min, double max)
        {
            var rng = RNG.Default;
            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextDouble(min, max), min, max);
            }
        }

        [Fact()]
        public void NextFloatTest()
        {
            var rng = RNG.Default;
            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextFloat(), 0.0f, 1.0f);
            }
        }

        [Theory()]
        [InlineData(1000f)]
        [InlineData(50000f)]
        [InlineData(7000000000f)]
        public void NextFloatTest1(float max)
        {
            var rng = RNG.Default;
            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextFloat(max), 0.0f, max);
            }
        }

        [Theory()]
        [InlineData(1000f, 3000f)]
        [InlineData(5000f, 500000f)]
        [InlineData(7000000000f, 9000000000f)]
        public void NextFloatTest2(float min, float max)
        {
            var rng = RNG.Default;
            for (int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextFloat(min, max), min, max);
            }
        }

        [Theory()]
        [InlineData(1000)]
        [InlineData(10000)]
        [InlineData(100000)]
        public void NextBoolTest(int iterations)
        {
            var rng = RNG.Default;

            List<bool> values = new List<bool>(iterations);
            for(int i = 0; i < iterations; i++)
            {
                values.Add(rng.NextBool());
            }

            // Expecting from 30% to 70% to be true
            Assert.InRange(values.Count(e => e == true), iterations * 0.30, iterations * 0.70);
        }

        [Theory()]
        [InlineData(1, 0, 1)]
        [InlineData(2, 0, 3)]
        [InlineData(4, 0, 16)]
        [InlineData(8, 0, 255)]
        [InlineData(16, 0, 65536)]
        [InlineData(32, 0, int.MaxValue)]
        public void NextBitsTest(byte bitsCount, int min, int max)
        {
            var rng = RNG.Default;

            for(int i = 0; i < Iterations; i++)
            {
                Assert.InRange(rng.NextBits(bitsCount), min, max);
            }
        }

        [Fact()]
        public void NextCharTest()
        {
            var rng = RNG.Default;

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.Lower);
                Assert.True(char.IsLower(c) && char.IsLetter(c));
            }

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.Upper);
                Assert.True(char.IsUpper(c) && char.IsLetter(c));
            }

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.Digit);
                Assert.True(char.IsDigit(c));
            }

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.Symbol);
                Assert.True(char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            }

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.Letter);
                Assert.True(char.IsLetter(c));
            }

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.LetterOrDigit);
                Assert.True(char.IsLetterOrDigit(c));
            }

            for (int i = 0; i < Iterations; i++)
            {
                char c = rng.NextChar(RandomCharKind.Any);
                Assert.False(char.IsWhiteSpace(c));
            }
        }

        [Theory()]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void NextStringTest(int length)
        {
            var rng = RNG.Default;
            Span<char> span = stackalloc char[length];

            rng.NextString(span);

            string str = span.ToString();
            Assert.Equal(length, str.Length);

            var chars = str.ToCharArray().Distinct().ToArray();
            int threshold = 5;

            for(int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                int repeatedCount = 0;

                for (int j = 0; j < str.Length; j++)
                {
                    if(c == str[j])
                    {
                        repeatedCount++;
                    }
                    else
                    {
                        Assert.True(repeatedCount < threshold, $"character {c} is repeated {repeatedCount} times.\n{str}");
                        repeatedCount = 0;
                    }
                }
            }
        }

        [Fact()]
        public void NextBytesTest()
        {
            var rng = RNG.Default;
            Span<byte> span = stackalloc byte[100];

            for (int i = 0; i < Iterations; i++)
            {
                rng.NextBytes(span);
                for(int j = 0; j < span.Length; j++)
                {
                    Assert.InRange(span[j], byte.MinValue, byte.MaxValue);
                }
            }
        }

        [Theory()]
        [InlineData(1000, 1)]
        [InlineData(10000, 2)]
        [InlineData(1000000, 4)]
        public void NextIntDistribuctionTest(int size, int threshold)
        {
            for(int i = 0; i < 100; i++)
            {
                var rng = RNG.Default;
                List<int> list = new List<int>(size);

                for (int j = 0; j < size; j++)
                {
                    list.Add(rng.NextInt());
                }

                var reduced = list.Distinct().Count();
                int equalValues = list.Count - reduced;
                Assert.True(equalValues >= 0 && equalValues <= threshold, $"Equal Values: {equalValues}");
            }
        }
    }
}