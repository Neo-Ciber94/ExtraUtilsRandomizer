using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExtraUtils.Randomizer
{
    /// <summary>
    /// Type of a random char.
    /// </summary>
    [Flags]
    public enum RandomCharKind
    {
        /// <summary>
        /// Upper case letters as A, B, C, D, etc.
        /// </summary>
        Upper = 1,
        /// <summary>
        /// Lower case letters as a, b, c, d, etc.
        /// </summary>
        Lower = 2,
        /// <summary>
        /// Numbers as 1, 2, 3, 4, etc.
        /// </summary>
        Digit = 4,
        /// <summary>
        /// Symbols as $, @, #, &, etc.
        /// </summary>
        Symbol = 8,
        /// <summary>
        /// Upper and lower case letters.
        /// </summary>
        Letter = Upper | Lower,
        /// <summary>
        /// Letters and numbers.
        /// </summary>
        LetterOrDigit = Letter | Digit,
        /// <summary>
        /// Any character
        /// </summary>
        Any = Upper | Lower | Digit | Symbol
    }

    /// <summary>
    /// A pseudorandom number generator using xorshift.
    /// </summary>
    public class RNG
    {
        private uint _state;

        /// <summary>
        /// Gets a default random number generator.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public static RNG Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                unchecked
                {
                    return new RNG((uint)(Stopwatch.GetTimestamp() * 12347));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RNG"/> struct.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public RNG(uint seed)
        {
            _state = seed;
            NextState();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RNG"/> struct.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public RNG(int seed) : this((uint)seed){ }

        /// <summary>
        /// Gets a pseudorandom int value from -2,147,483,648 to 2,147,483,647.
        /// </summary>
        /// <returns>A random int value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt()
        {
            return NextState();
        }

        /// <summary>
        /// Gets a pseudorandom int value from 0 to max (inclusive).
        /// </summary>
        /// <param name="max">The maximum value (inclusive).</param>
        /// <returns>A random int value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt(int max)
        {
            return NextInt(0, max);
        }

        /// <summary>
        /// Gets a pseudorandom int value from min (inclusive) to max (inclusive).
        /// </summary>
        /// <param name="min">The minimum value inclusive.</param>
        /// <param name="max">The maximum value inclusive.</param>
        /// <returns>A random int value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        public int NextInt(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException($"min > max: {min} > {max}");
            }

            long n = (long)max - min;

            if(n < int.MaxValue)
            {
                return (int)(NextDouble() * (n + 1)) + min;
            }
            else
            {
                do
                {
                    n = NextInt();
                }
                while (n < min || n > max);
            }

            return (int)n;
        }

        /// <summary>
        /// Gets a pseudorandom long value from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.
        /// </summary>
        /// <returns>A random long value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long NextLong()
        {
            return NextInt() + ((long)NextInt() << 32);
        }

        /// <summary>
        /// Gets a pseudorandom long value between 0 and max (inclusive)
        /// </summary>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random long value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long NextLong(long max)
        {
            return NextLong(0, max);
        }

        /// <summary>
        /// Gets a pseudorandom long value between min (inclusice) and max (inclusive)
        /// </summary>
        /// <param name="min">The minimun value inclusive.</param>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random long value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        public long NextLong(long min, long max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException($"min > max: {min} > {max}");
            }

            long n = max - min;

            if(n >= 0)
            {
                return (long)(NextDouble() * (n + 1)) + min;
            }
            else
            {
                do
                {
                    n = NextLong();
                }
                while (n < min || n > max);
            }

            return n;
        }

        /// <summary>
        /// Gets a pseudorandom long value from 0 to 4,294,967,295.
        /// </summary>
        /// <returns>A random uint value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NextUInt()
        {
            return (uint)NextState();
        }

        /// <summary>
        /// Gets a pseudorandom uint value between 0 and max (inclusive)
        /// </summary>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random uint value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NextUInt(uint max)
        {
            return NextUInt(0, max);
        }

        /// <summary>
        /// Gets a pseudorandom uint value between min (inclusice) and max (inclusive)
        /// </summary>
        /// <param name="min">The minimun value inclusive.</param>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random uint value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NextUInt(uint min, uint max)
        {
            return (uint)NextInt((int)min, (int)max);
        }

        /// <summary>
        /// Gets a pseudorandom ulong value from 0 to 18,446,744,073,709,551,615.
        /// </summary>
        /// <returns>A random ulong value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NextULong()
        {
            return (ulong)NextInt() + ((ulong)NextInt() << 32);
        }

        /// <summary>
        /// Gets a pseudorandom ulong value between 0 and max (inclusive)
        /// </summary>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random ulong value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NextULong(ulong max)
        {
            return NextULong(0, max);
        }

        /// <summary>
        /// Gets a pseudorandom ulong value between min (inclusice) and max (inclusive)
        /// </summary>
        /// <param name="min">The minimun value inclusive.</param>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random ulong value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NextULong(ulong min, ulong max)
        {
            return (ulong)NextLong((long)min, (long)max);
        }

        /// <summary>
        /// Gets a pseudorandom double value between 0 (inclusive) and 1 (exclusive).
        /// </summary>
        /// <returns>A random double value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble()
        {
            return (NextState() & int.MaxValue) * (1.0 / int.MaxValue);
        }

        /// <summary>
        /// Gets a pseudorandom double value between 0 and max (exclusive).
        /// </summary>
        /// <param name="max">The maximum value exclusive.</param>
        /// <returns>A random double value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble(double max)
        {
            return NextDouble(0, max);
        }

        /// <summary>
        /// Gets a pseudorandom double value between min (inclusive) and max (exclusive).
        /// </summary>
        /// <param name="min">The minimum value inclusive.</param>
        /// <param name="max">The maximum value exclusive.</param>
        /// <returns>A random double value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble(double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException($"min > max: {min} > {max}");
            }

            return NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Gets a pseudorandom float value between 0 (inclusive) and 1 (exclusive).
        /// </summary>
        /// <returns>A random float value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat()
        {
            return (NextState() & int.MaxValue) * (1f / int.MaxValue);
        }

        /// <summary>
        /// Gets a pseudorandom float value between 0 and max (exclusive).
        /// </summary>
        /// <param name="max">The maximum value exclusive.</param>
        /// <returns>A random float value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat(float max)
        {
            return NextFloat(0, max);
        }

        /// <summary>
        /// Gets a pseudorandom float value between min (inclusive) and max (exclusive).
        /// </summary>
        /// <param name="min">The minimum value inclusive.</param>
        /// <param name="max">The maximum value exclusive.</param>
        /// <returns>A random float value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat(float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException($"min > max: {min} > {max}");
            }

            return NextFloat() * (max - min) + min;
        }

        /// <summary>
        /// Gets a pseudorandom <see langword="true"/> or <see langword="false"/>.
        /// </summary>
        /// <returns>A random bool value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextBool()
        {
            return NextInt(1) == 1;
        }

        /// <summary>
        /// Gets a pseudorandom <see langword="int"/> with 0 to 'bitCount' number of bits,
        /// where bitsCount must be between 0 to 32.
        /// </summary>
        /// <param name="bitsCount">Max number of bits of the pseudorandom numbers</param>
        /// <returns>A random int value with 0 to 'bitCount' number of bits.</returns>
        public int NextBits(byte bitsCount)
        {
            Debug.Assert(bitsCount >= 0 && bitsCount <= 32);

            int value = 0;
            for (int i = 0; i < bitsCount; i++)
            {
                value |= (NextInt() & int.MaxValue) >> (32 - (i + 2));
            }
            return value;
        }

        /// <summary>
        /// Gets a pseudorandom <see langword="char"/>.
        /// </summary>
        /// <returns>A random char value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char NextChar()
        {
            return NextChar(RandomCharKind.LetterOrDigit);
        }

        /// <summary>
        /// Gets a pseudorandom <see langword="char"/>.
        /// </summary>
        /// <param name="charKind">Types of the character.</param>
        /// <returns>A random char value.</returns>
        public char NextChar(RandomCharKind charKind)
        {
            const string LettersUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string LettersLower = "abcdefghijklmnopqrstuvwxyz";
            const string Digits = "0123456789";
            const string Symbols = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

            Span<RandomCharKind> flags = stackalloc RandomCharKind[4];
            int flagCount = 0;

            if (HasFlag(charKind, RandomCharKind.Digit))
            {
                flags[flagCount++] = RandomCharKind.Digit;
            }
            if (HasFlag(charKind, RandomCharKind.Upper))
            {
                flags[flagCount++] = RandomCharKind.Upper;
            }
            if (HasFlag(charKind, RandomCharKind.Lower))
            {
                flags[flagCount++] = RandomCharKind.Lower;
            }
            if (HasFlag(charKind, RandomCharKind.Symbol))
            {
                flags[flagCount++] = RandomCharKind.Symbol;
            }

            RandomCharKind flag = flags[NextInt(0, flagCount - 1)];

            return flag switch
            {
                RandomCharKind.Upper => LettersUpper[NextInt(0, LettersUpper.Length - 1)],
                RandomCharKind.Lower => LettersLower[NextInt(0, LettersLower.Length - 1)],
                RandomCharKind.Digit => Digits[NextInt(0, Digits.Length - 1)],
                RandomCharKind.Symbol => Symbols[NextInt(0, Symbols.Length - 1)],
                _ => default,
            };

            static bool HasFlag(RandomCharKind e, RandomCharKind flag)
            {
                return (e & flag) != 0;
            }
        }

        /// <summary>
        /// Fills the <see cref="Span{T}"/> with random chars.
        /// </summary>
        /// <param name="destination">The destination.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NextString(Span<char> destination)
        {
            for (int i = 0; i < destination.Length; ++i)
            {
                destination[i] = NextChar();
            }
        }

        /// <summary>
        /// Fills the <see cref="Span{T}"/> with random characters.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="charKind">Types of the character.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NextString(Span<char> destination, RandomCharKind charKind)
        {
            for (int i = 0; i < destination.Length; ++i)
            {
                destination[i] = NextChar(charKind);
            }
        }

        /// <summary>
        /// Fills the <see cref="Span{T}"/> with random bytes.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="destination">The destination.</param>
        public void NextBytes(Span<byte> destination)
        {
            int length = destination.Length;
            int i = 0;

            if(length > 4)
            {
                ref int cur = ref Unsafe.As<byte, int>(ref destination.GetPinnableReference());
                while(length > 4)
                {
                    Unsafe.AddByteOffset(ref cur, (IntPtr)i) = NextInt();
                    length -= 4;
                    i += 4;
                }
            }

            for (; i < length; ++i)
            {
                destination[i] = (byte)(NextInt() & byte.MaxValue);
            }
        }

        private int NextState()
        {
            //Add a prime number to ensure non-zero state
            unchecked
            {
                _state += 23457013;
            }

            _state ^= _state << 13;
            _state ^= _state >> 17;
            _state ^= _state << 5;
            return (int)(_state ^ int.MinValue);
        }
    }
}
