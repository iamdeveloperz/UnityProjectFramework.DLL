
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityProjectFramework.Core.UpdateLoop
{
    /// <summary>
    /// Represents a collection that allows addition, removal, and iteration of items efficiently
    /// while preserving the ability to iterate without breaking enumeration.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public class IteratorList<T> : IReadOnlyList<T>
    {
        #region Fields

        private readonly List<T> _list = new List<T>();
        private readonly Dictionary<T, int> _indexMap = new Dictionary<T, int>();
        private int _loopIndex;

        /// <summary>
        /// Gets the total number of elements contained in the collection.
        /// </summary>
        /// <value>
        /// An integer representing the number of elements currently stored in the collection.
        /// </value>
        public int Count => _list.Count;

        /// <summary>
        /// Gets the element at the specified index in the collection. If the index is out of range, an exception is thrown.
        /// </summary>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>The element at the specified index in the iterator list.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the specified index is out of range.</exception>
        public T this[int index]
            => (index >= 0 && index < Count ? _list[index] : default) ?? throw new InvalidOperationException();

        #endregion

        /// <summary>
        /// Adds the specified value to the collection if it does not already exist.
        /// </summary>
        /// <param name="value">The value to be added to the collection.</param>
        /// <returns>
        /// Returns true if the value was successfully added; otherwise, false if the value already exists in the collection.
        /// </returns>
        public bool Add(T value)
        {
            if (_indexMap.ContainsKey(value))
            {
                return false;
            }

            _list.Add(value);
            _indexMap.Add(value, _list.Count - 1);
            return true;
        }

        /// <summary>
        /// Removes the specified value from the collection if it exists.
        /// </summary>
        /// <param name="value">The value to be removed from the collection.</param>
        /// <returns>
        /// Returns true if the value was successfully removed; otherwise, false if the value was not found in the collection.
        /// </returns>
        public bool Remove(T value)
        {
            if (!_indexMap.Remove(value, out var indexToRemove))
            {
                return false;
            }
            
            if (indexToRemove == _loopIndex)
            {
                _loopIndex--;
            }
            else if (indexToRemove < _loopIndex)
            {
                _list.Swap(_loopIndex, indexToRemove, out T swappedValue);
                _indexMap[swappedValue] = indexToRemove;
                indexToRemove = _loopIndex;
                _loopIndex--;
            }

            _list.RemoveAtSwapBack(indexToRemove, out T swappedBack);
            if (swappedBack != null)
            {
                _indexMap[swappedBack] = indexToRemove;
            }

            return true;
        }

        /// <summary>
        /// Removes all elements from the collection.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
            _indexMap.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator for the collection, allowing efficient iteration over its elements.
        /// </returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Enumerator Custom

        /// <summary>
        /// Provides an enumerator for iterating over the elements of an <see cref="IteratorList{T}"/>.
        /// </summary>
        public readonly struct Enumerator : IEnumerator<T>
        {
            private readonly IteratorList<T> _list;

            /// <summary>
            /// Gets the element in the collection at the current iteration position.
            /// </summary>
            /// <value>
            /// at the current iteration point within the collection.
            /// </value>
            public T Current => _list[_list._loopIndex];
            object IEnumerator.Current => Current ?? throw new InvalidOperationException();

            public Enumerator(IteratorList<T> list)
            {
                _list = list;
                Reset();
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// Returns true if the enumerator was successfully advanced to the next element;
            /// otherwise, false if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (_list._loopIndex >= _list.Count - 1)
                {
                    return false;
                }
                
                _list._loopIndex++;
                return true;
            }

            public void Reset()
            {
                _list._loopIndex = -1;
            }
            
            public void Dispose() { }
        }

        #endregion
    }
}
