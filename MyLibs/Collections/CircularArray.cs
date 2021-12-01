namespace MyLibs.Collections
{
    /// <summary>
    /// Wrapper around an <see cref="System.Array"/> that uses <see cref="CollectionUtilities.GetCircularIndex(int, int)"/> for all indexing.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>.</typeparam>
    public class CircularArray<T>
    {
        #pragma warning disable CA1819 // Properties should not return arrays
        /// <summary>
        /// The underlying array for the CircularArray.
        /// </summary>
        public T[] Array { get; }
        #pragma warning restore CA1819 // Properties should not return arrays

        /// <summary>
        /// Initializes a new <see cref="System.Array"/> for the <see cref="CircularArray{T}"/>.
        /// </summary>
        /// <param name="length">The length of the array.</param>
        public CircularArray(int length)
        {
            Array = new T[length];
        }

        /// <summary>
        /// Wraps a <see cref="CircularArray{T}"/> around a preexisting array.
        /// </summary>
        /// <param name="array">The array to use for circular indexing.</param>
        public CircularArray(T[] array)
        {
            Array = array;
        }

        /// <summary>
        /// Access array using a circular index.
        /// </summary>
        /// <param name="index">Index into array. Will be converted into a circular index.</param>
        /// <returns>The item located at the circular index.</returns>
        public T this[int index]
        {
            get => Array[CollectionUtilities.GetCircularIndex(index, Array.Length)];
            set => Array[CollectionUtilities.GetCircularIndex(index, Array.Length)] = value;
        }

        /// <summary>
        /// Access array using a circular index.
        /// </summary>
        /// <param name="index">Index into array. Will be converted into a circular index.</param>
        /// <returns>The item located at the circular index.</returns>
        public T this[long index]
        {
            get => Array[CollectionUtilities.GetCircularIndex(index, Array.Length)];
            set => Array[CollectionUtilities.GetCircularIndex(index, Array.Length)] = value;
        }
    }
}
