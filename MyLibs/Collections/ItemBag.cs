using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLibs.Collections
{
    /// <summary>
    /// Wrapper around a List that optimizes removals by swapping the item to be removed with the item at the end of the list before removing.
    /// This means that the order of elements in the list can change after removals.
    /// The intended use for this data structure is for collections where the order of the items in the List doesn't matter and you only need to add, remove, and iterate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class ItemBag<T> : IEnumerable<T>
    {
        /// <summary>
        /// The underlying list for the ItemBag.
        /// </summary>
        public List<T> List { get; }

        /// <summary>
        /// Initializes a new <see cref="ItemBag{T}"/> that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new <see cref="ItemBag{T}"/> can initially store before needing to resize.</param>
        public ItemBag(int capacity = 16)
        {
            List = new List<T>(capacity);
        }

        /// <summary>
        /// Initializes a new <see cref="ItemBag{T}"/> that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="ItemBag{T}"/>.</param>
        public ItemBag(IEnumerable<T> collection)
        {
            List = new List<T>(collection);
        }

        /// <summary>
        /// Wraps an <see cref="ItemBag{T}"/> around a preexisting <see cref="List{T}"/>.
        /// </summary>
        /// <param name="list">The <see cref="List{T}"/> to wrap the <see cref="ItemBag{T}"/> around.</param>
        public ItemBag(List<T> list)
        {
            List = list;
        }

        public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ItemBag{T}"/> .
        /// </summary>
        public int Count => List.Count;

        /// <summary>
        /// Gets or sets the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity => List.Capacity;

        /// <summary>
        /// Adds an object to the end of the <see cref="ItemBag{T}"/>.
        /// </summary>
        /// <param name="item">The object to be added to the end of the <see cref="ItemBag{T}"/>. The value can be null for reference types.</param>
        public void Add(T item) => List.Add(item);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ItemBag{T}"/>.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="ItemBag{T}"/>. The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        public void AddRange(IEnumerable<T> collection) => List.AddRange(collection);

        /// <summary>
        /// Removes all elements from the <see cref="ItemBag{T}"/>.
        /// </summary>
        public void Clear() => List.Clear();

        private void Swap(int a, int b)
        {
            var temp = List[a];
            List[a] = List[b];
            List[b] = temp;
        }

        private void RemoveAt(int index)
        {
            // Swap item to end, then remove from end so no copying takes place.
            Swap(index, List.Count - 1);
            List.RemoveAt(List.Count - 1);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ItemBag{T}"/>. The removed item is swapped with the item at the end of the <see cref="ItemBag{T}"/> so that no copying takes place.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ItemBag{T}"/>. The value can be null for reference types.</param>
        /// <returns><see langword="true"/> if item is successfully removed; otherwise, false. This method also returns <see langword="false"/> if item was not found in the <see cref="ItemBag{T}"/>.</returns>
        public bool Remove(T item)
        {
            var index = List.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Remove all occurrences of items that are contained the specified collection from the <see cref="ItemBag{T}"/>.
        /// </summary>
        /// <param name="items">The items to be removed from the <see cref="ItemBag{T}"/>.</param>
        public void RemoveAll(IEnumerable<T> items)
        {
            int i = 0;
            while (i < List.Count)
            {
                bool found = false;
                foreach (var item in items)
                {
                    if (List[i].Equals(item))
                    {
                        RemoveAt(i);
                        found = true;
                        break;
                    }
                }

                // When items are removed, a new item is swapped into the current position, so only increment i if no removal.
                if (!found)
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Remove all occurrences of items that match the specified predicate from the <see cref="ItemBag{T}"/>.
        /// </summary>
        /// <param name="match">A function to test each element for a condition. If the element matches the predicate, then the element is removed from the <see cref="ItemBag{T}"/>.</param>
        public void RemoveAll(Predicate<T> match)
        {
            int i = 0;
            while (i < List.Count)
            {
                if (match(List[i]))
                {
                    RemoveAt(i);
                }
                else
                {
                    // When items are removed, a new item is swapped into the current position, so only increment i if no removal.
                    i++;
                }
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the <see cref="ItemBag{T}"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to perform on each element of the <see cref="ItemBag{T}"/>.</param>
        public void ProcessItems(Action<T> action)
        {
            for (int i = 0; i < List.Count; i++)
            {
                action(List[i]);
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the <see cref="ItemBag{T}"/>. After the action is performed, the element is then tested against the predicate. If the element matches the predicate, it is removed from the <see cref="ItemBag{T}"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to perform on each element of the <see cref="ItemBag{T}"/>.</param>
        /// <param name="removeIfMatch">A function to test each element for a condition. If the element matches the predicate, then the element is removed from the <see cref="ItemBag{T}"/> after being processed.</param>
        public void ProcessItems(Action<T> action, Predicate<T> removeIfMatch)
        {
            int i = 0;
            while (i < List.Count)
            {
                action(List[i]);

                if (removeIfMatch(List[i]))
                {
                    RemoveAt(i);
                }
                else
                {
                    // When items are removed, a new item is swapped into the current position, so only increment i if no removal.
                    i++;
                }
            }
        }
    }
}
