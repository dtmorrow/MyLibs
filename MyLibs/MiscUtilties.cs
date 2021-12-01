using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MyLibs
{
    /// <summary>
    /// Miscellaneous utility methods.
    /// </summary>
    public static class MiscUtilties
    {
        /// <summary>
        /// Generate prime numbers from 2 to a max value.
        /// </summary>
        /// <param name="max">The maximum number to generate prime numbers up to (inclusive).</param>
        /// <returns>An IEnumerable&lt;int&gt; containing prime numbers from 2 to max.</returns>
        public static IEnumerable<int> GeneratePrimes(int max = int.MaxValue)
        {
            var composites = new List<(int composite, int rootPrime)>();
            for (int i = 2; i <= max; i++)
            {
                bool isPrime = true;

                // Test to see if i is a composite
                for (int c = 0; c < composites.Count; c++)
                {
                    // If i is composite, update composite to be next multiple of composite
                    if (composites[c].composite == i)
                    {
                        isPrime = false;
                        composites[c] = (composites[c].composite + composites[c].rootPrime, composites[c].rootPrime);
                    }
                }
                
                if (isPrime)
                {
                    yield return i;
                    composites.Add((i + i, i));
                }
            }
        }

        /// <summary>
        /// Calculate the prime factors of a number.
        /// </summary>
        /// <param name="value">The value to calculate the prime factors of.</param>
        /// <returns>An IEnumerable&lt;KeyValuePairs&lt;int, int&gt;&gt; with the Prime as the Key, and the Power as the Value.</returns>
        public static IEnumerable<KeyValuePair<int, int>> GetPrimeFactors(int value)
        {
            if (value <= 1)
            {
                return Enumerable.Empty<KeyValuePair<int, int>>();
            }

            var factors = new Dictionary<int, int>(); // Prime -> Power

            int i = 2;
            while (value != 1)
            {
                int quot = Math.DivRem(value, i, out int rem);

                // If evenly divisible, update value with new numerator and add/increment factor. Else, try next denominator
                if (rem == 0)
                {
                    value = quot;

                    if (factors.ContainsKey(i))
                    {
                        factors[i]++;
                    }
                    else
                    {
                        factors[i] = 1;
                    }
                }
                else
                {
                    i++;
                }
            }

            return factors;
        }
    }
}
