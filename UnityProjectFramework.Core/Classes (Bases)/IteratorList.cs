
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityProjectFramework.Core
{
    public class IteratorList<T> : IReadOnlyList<T>
    {
        #region Fields

        private readonly List<T> _list = new List<T>();
        private readonly Dictionary<T, int> _indexMap = new Dictionary<T, int>();
        private int _index;

        public int Count => _list.Count;

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= 0 && index < Count)
                {
                    return _list[index];
                }

                return default;
            }
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Remove(T value)
        {
            if(!_indexMap.Remove(value, out var indexToRemove))
            {
                return false;
            }

            if (indexToRemove == _index)
            {
                --_index;
            }
            else if (indexToRemove < _index)
            {
                _list.Swap(_index, indexToRemove, out var swappedValue);
                _indexMap[swappedValue] = indexToRemove;
                indexToRemove = _index;
                --_index;
            }
            
            _list.RemoveAtSwapBack(indexToRemove, out var swappedBack);;
            if (swappedBack != null)
            {
                _indexMap[swappedBack] = indexToRemove;
            }

            return true;
        }

        public void Clear()
        {
            _list.Clear();
            _indexMap.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        #region Enumerator

        public readonly struct Enumerator : IEnumerator<T>
        {
            private readonly IteratorList<T> _list;
            
            object? IEnumerator.Current => Current;

            public T Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _list[_list._index];
            }
            
            public Enumerator(IteratorList<T> list)
            {
                _list = list;
                Reset();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                if (_list._index >= _list.Count - 1)
                {
                    return false;
                }
                
                ++_list._index;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Reset()
            {
                _list._index = -1;
            }
            
            public void Dispose() { }
        }

        #endregion
    }
}