using System;
using System.Runtime.CompilerServices;

namespace ExtraUtils.Randomizer.Utils
{
    public static class PrimeNumberHelper
    {
        public static long NextPrime(long num)
        {
            while (true)
            {
                if (IsPrime(++num))
                {
                    break;
                }
            }

            return num;
        }

        public static bool IsPrime(long value)
        {
            if(value < 0)
            {
                throw new ArgumentException($"value must be positive: {value}", nameof(value));
            }

            if(value == 0 || value == 1)
            {
                return false;
            }

            if(value == 2 || value == 3)
            {
                return true;
            }

            if(value % 2 == 0)
            {
                return false;
            }

            for(int i = 4; i < value; i++)
            {
                if(Mod(value, i) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long Mod(long input, long ceil)
        {
            return input > ceil ? input % ceil : input;
        }
    }
}
