using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibs.Collections
{
    /// <summary>
    /// Methods for use with Collections.
    /// </summary>
    public static class CollectionUtilities
    {
        /// <summary>
        /// Get the circular index for a collection. Indexes that are greater than the length of the collection wrap around to the beginning of the collection (e.g. length + 1 = 0). Negative indexes wrap around to the end of the collection (e.g. -1 = length - 1).
        /// </summary>
        /// <param name="i">The index to convert to a circular index.</param>
        /// <param name="length">The length of the collection.</param>
        /// <returns>The circular index for the provided index.</returns>
        public static int GetCircularIndex(int i, int length)
        {
            // -- Original --
            //int remainder = i % length;

            //if (remainder < 0)
            //{
            //    remainder += length;
            //}

            //return remainder;
            // --------------
            // -- Optimized --

            int remainder = i % length;
            //int sign = remainder >> 31;  // Arithmetic shift all bits right. If 'remainder' is positive, then 'sign' is 0; if 'remainder' is negative, then 'sign' is -1.
            //int add = sign & length;     // ANDing 'length' with 0 results in 0; ANDing 'length' with -1 results in 'length'.
            //int index = remainder + add; // If 'remainder' is positive, then this will just be adding with 0; If 'remainder' is negative, then this will be adding 'remainder' to 'length'.
            return ((remainder >> 31) & length) + remainder;
        }

        /// <summary>
        /// Get the circular index for a collection. Indexes that are greater than the length of the collection wrap around to the beginning of the collection (e.g. length + 1 = 0). Negative indexes wrap around to the end of the collection (e.g. -1 = length - 1).
        /// </summary>
        /// <param name="i">The index to convert to a circular index.</param>
        /// <param name="length">The length of the collection.</param>
        /// <returns>The circular index for the provided index.</returns>
        public static int GetCircularIndex(long i, int length)
        {
            int remainder = (int)(i % length);
            return ((remainder >> 31) & length) + remainder;
        }
    }
}
