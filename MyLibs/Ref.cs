using System;

namespace MyLibs
{
    // Source: https://stackoverflow.com/a/2258102
    /// <summary>
    /// Provides a way to read and write to out-of-scope variables, including value types.
    /// </summary>
    /// <typeparam name="T">The type of the variable to reference.</typeparam>
    public class Ref<T>
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;

        // Usage: var iRef = new Ref<int>(() => i, (value) => i = value);
        /// <summary>
        /// Create a reference to a variable.
        /// </summary>
        /// <param name="get">A getter to a variable.</param>
        /// <param name="set">A setter to a variable.</param>
        public Ref(Func<T> get, Action<T> set)
        {
            getter = get;
            setter = set;
        }

        /// <summary>
        /// The reference variable.
        /// </summary>
        public T Value
        {
            get => getter();
            set => setter(value);
        }
    }
}
