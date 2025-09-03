
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityProjectFramework.Core.UpdateLoop
{
    public class IteratorList<T> : IReadOnlyList<T>
    {
        #region Fields

        private readonly List<T> _list = new List<T>();
        private readonly Dictionary<T, int> _indexMap = new Dictionary<T, int>();
        private int _loopIndex;
        
        public int Count => _list.Count;
        
        public T this[int index] 
            => (index >= 0 && index < Count ? _list[index] : default) ?? throw new InvalidOperationException();

        #endregion
        
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
        
        public void Clear()
        {
            _list.Clear();
            _indexMap.Clear();
        }
        
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public readonly struct Enumerator : IEnumerator<T>
        {
            private readonly IteratorList<T> _list;
            public T Current => _list[_list._loopIndex];
            object IEnumerator.Current => Current ?? throw new InvalidOperationException();

            public Enumerator(IteratorList<T> list)
            {
                _list = list;
                Reset();
            }

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
    }
}
