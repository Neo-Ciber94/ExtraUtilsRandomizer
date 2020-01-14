﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
        LetterAndDigit = Letter | Digit,
        /// <summary>
        /// Any character
        /// </summary>
        Any = Upper | Lower | Digit | Symbol
    }

    /// <summary>
    /// A pseudorandom number generator.
    /// </summary>
    public struct RNG
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
                    return new RNG((uint)(Stopwatch.GetTimestamp() * 12345));
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
        /// Gets a random int value from -2,147,483,648 to 2,147,483,647.
        /// </summary>
        /// <returns>A random int value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt()
        {
            return NextState();
        }

        /// <summary>
        /// Gets a random int value from 0 to max (inclusive).
        /// </summary>
        /// <param name="max">The maximum value (inclusive).</param>
        /// <returns>A random int value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt(int max)
        {
            return NextInt(0, max);
        }

        /// <summary>
        /// Gets a random int value from min (inclusive) to max (inclusive).
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

            if(max == min)
            {
                return min;
            }

            if((max & min) == 0)
            {
                do
                {
                    int n = NextInt();

                    if (n >= min && n <= max)
                    {
                        return n;
                    }
                }
                while (true);
            }

            return (int)(NextDouble() * ((max - min) + 1)) + min;
        }

        /// <summary>
        /// Gets a random long value from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.
        /// </summary>
        /// <returns>A random long value.</returns>
        public long NextLong()
        {
            unsafe
            {
                byte* buffer = stackalloc byte[8];
                for(int i = 0; i < 8; i++)
                {
                    buffer[i] = (byte)(NextInt() & byte.MaxValue);
                }

                return *(long*)buffer;
            }
        }

        /// <summary>
        /// Gets a random long value between 0 and max (inclusive)
        /// </summary>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random long value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long NextLong(int max)
        {
            return NextLong(0, max);
        }

        /// <summary>
        /// Gets a random long value between min (inclusice) and max (inclusive)
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

            if (max == min)
            {
                return min;
            }

            if ((max & min) == 0)
            {
                do
                {
                    long n = NextLong();

                    if (n >= min && n <= max)
                    {
                        return n;
                    }
                }
                while (true);
            }

            return (long)(NextDouble() * (max - min + 1)) + min;
        }

        /// <summary>
        /// Gets a random long value from 0 to 4,294,967,295.
        /// </summary>
        /// <returns>A random uint value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NextUInt()
        {
            return (uint)NextState();
        }

        /// <summary>
        /// Gets a random uint value between 0 and max (inclusive)
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
        /// Gets a random uint value between min (inclusice) and max (inclusive)
        /// </summary>
        /// <param name="min">The minimun value inclusive.</param>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random uint value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        public uint NextUInt(uint min, uint max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException($"min > max: {min} > {max}");
            }

            if (max == min)
            {
                return min;
            }

            if ((max & min) == 0)
            {
                do
                {
                    uint n = NextUInt();

                    if (n >= min && n <= max)
                    {
                        return n;
                    }
                }
                while (true);
            }

            return (uint)(NextDouble() * ((max - min) + 1)) + min;
        }

        /// <summary>
        /// Gets a random ulong value from 0 to 18,446,744,073,709,551,615.
        /// </summary>
        /// <returns>A random ulong value.</returns>
        public ulong NextULong()
        {
            unsafe
            {
                byte* buffer = stackalloc byte[8];
                for (int i = 0; i < 8; i++)
                {
                    buffer[i] = (byte)(NextInt() & byte.MaxValue);
                }

                return *(ulong*)buffer;
            }
        }

        /// <summary>
        /// Gets a random ulong value between 0 and max (inclusive)
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
        /// Gets a random ulong value between min (inclusice) and max (inclusive)
        /// </summary>
        /// <param name="min">The minimun value inclusive.</param>
        /// <param name="max">The maximun value inclusive.</param>
        /// <returns>A random ulong value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If min is greater than max.</exception>
        public ulong NextULong(ulong min, ulong max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException($"min > max: {min} > {max}");
            }

            if (max == min)
            {
                return min;
            }

            if ((max & min) == 0)
            {
                do
                {
                    ulong n = NextULong();

                    if (n >= min && n <= max)
                    {
                        return n;
                    }
                }
                while (true);
            }

            return (ulong)(NextDouble() * ((max - min) + 1)) + min;
        }

        /// <summary>
        /// Gets a random double value between 0 (inclusive) and 1 (exclusive).
        /// </summary>
        /// <returns>A random double value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble()
        {
            return Math.Abs(NextState()) * (1.0 / int.MaxValue);
        }

        /// <summary>
        /// Gets a random double value between 0 and max (exclusive).
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
        /// Gets a random double value between min (inclusive) and max (exclusive).
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
        /// Gets a random float value between 0 (inclusive) and 1 (exclusive).
        /// </summary>
        /// <returns>A random float value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float NextFloat()
        {
            return Math.Abs(NextState()) * (1f / int.MaxValue);
        }

        /// <summary>
        /// Gets a random float value between 0 and max (exclusive).
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
        /// Gets a random float value between min (inclusive) and max (exclusive).
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
        /// Gets a random <see langword="true"/> or <see langword="false"/>.
        /// </summary>
        /// <returns>A random bool value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextBool()
        {
            return NextInt(1) == 1;
        }

        /// <summary>
        /// Gets a random <see langword="char"/>.
        /// </summary>
        /// <returns>A random char value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char NextChar()
        {
            return NextChar(RandomCharKind.LetterAndDigit);
        }

        /// <summary>
        /// Gets a random <see langword="char"/>.
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