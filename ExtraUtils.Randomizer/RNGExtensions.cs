using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ExtraUtils.Randomizer
{
    /// <summary>
    /// Extensions for <see cref="RNG"/>.
    /// </summary>
    public static partial class RNGExtensions
    {
        /// <summary>
        /// Shuffles the elements in the list.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="list">The list.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list)
        {
            Shuffle(list, RNG.Default);
        }

        /// <summary>
        /// Shuffles the elements in the list.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="rng">The RNG to use.</param>
        public static void Shuffle<T>(this IList<T> list, RNG rng)
        {
            int length = list.Count;
            for(int i = 0; i < length; i++)
            {
                int index = rng.NextInt(i, length - 1);
                T temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
        }

        /// <summary>
        /// Gets a random value between the given range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="rng">The RNG.</param>
        /// <returns>A value between the given range.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Random(this Range range)
        {
            return Random(range, RNG.Default);
        }

        /// <summary>
        /// Gets a random value between the given range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="rng">The RNG provider.</param>
        /// <returns>A value within the given range.</returns>
        public static int Random(this Range range, RNG rng)
        {
            Index start = range.Start;
            Index end = range.End;
            return rng.NextInt(start.Value, end.Value);
        }
    }
}
