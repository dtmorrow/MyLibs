using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibs.Collections
{
    /// <summary>
    /// Extensions for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines where a sequence contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to check for emptiness.</param>
        /// <returns><see langword="true"/> if the source sequence is empty; otherwise, <see langword="false"/>.</returns>
        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// Determines whether all elements of a sequence do not satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns><see langword="true"/> if all elements in the source sequence do not pass the test in the specified predicate; otherwise, <see langword="false"/>.</returns>
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
        }

        /// <summary>
        /// Performs the specified action on each element of the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> on whose elements the action is performed.</param>
        /// <param name="action">The <see cref="Action{T}"/> to perform on each element of the sequence.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Converts a single item to an <see cref="IEnumerable{T}"/> of that item.
        /// </summary>
        /// <typeparam name="T">The type of elements for the <see cref="IEnumerable{T}"/></typeparam>
        /// <param name="item">The item to return as an <see cref="IEnumerable{T}"/></param>
        /// <returns>An <see cref="IEnumerable{T}"/> with the only item being the supplied item.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}
