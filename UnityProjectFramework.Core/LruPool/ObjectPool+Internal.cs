
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityProjectFramework.Core.LruPool
{
    public sealed partial class LruPool<T>
    {
        #region Structure

        private struct EvictablePoolEntry
        {
            public T Item;
            public int Prev, Next;
            public int NextFree;
            public long LastUseTicks;
            public byte InUse;
        }

        #endregion
        
        private void ApplyEvictionOnReturn_NoUnityCalls()
        {
            List<T>? toDestroy = null;
            if (_policy == EvictionPolicy.CapacityOnly)
            {
                while (_idleCount > 0 && _count > _capacity)
                {
                    var idx = _lruHead;
                    RemoveFromLru(idx);
                    var td = DetachEntry(idx);
                    if (td != null) (toDestroy ??= new List<T>()).Add(td);
                }
            }
            else if (_policy == EvictionPolicy.CapacityGatedTtl)
            {
                if (_count <= _capacity || _ttlMs <= 0 || _lruHead == -1) return;
                
                var now = Stopwatch.GetTimestamp();
                while (_count > _capacity && _idleCount > 0 && _lruHead != -1)
                {
                    var idx = _lruHead;
                    ref var e = ref _entries[idx];
                    var ageMs = (now - e.LastUseTicks) * _tickToMs;
                    if (ageMs < _ttlMs) break;

                    RemoveFromLru(idx);
                    var td = DetachEntry(idx);
                    if (td != null) (toDestroy ??= new List<T>()).Add(td);
                }
            }
            
            if (toDestroy != null) 
            { 
                foreach (var it in toDestroy) 
                    TryDestroyDirect(it); 
            }
        }
        
        private void ScanTtlEntry(object? _)
        {
            if (_isDisposed) return;
            
            List<T>? toDestroy = null;
            lock (_gate)
            {
                if (_policy != EvictionPolicy.CapacityGatedTtl || _ttlMs <= 0) return;
                if (!(_count > _capacity && _idleCount > 0 && _lruHead != -1)) return;
                
                var now = Stopwatch.GetTimestamp();
                const int MaxBatch = 32;
                var removed = 0;

                while (_count > _capacity && _idleCount > 0 && _lruHead != -1 && removed < MaxBatch)
                {
                    var idx = _lruHead;
                    ref var entry = ref _entries[idx];
                    var ageMs = (now - entry.LastUseTicks) * _tickToMs;
                    
                    if (ageMs < _ttlMs) break;

                    RemoveFromLru(idx);
                    var toDestroyItem = DetachEntry(idx);
                    if (toDestroyItem != null)
                    {
                        (toDestroy ??= new List<T>()).Add(toDestroyItem);
                        ++removed;
                    }
                }
            }

            if (toDestroy != null) 
            { 
                foreach (var it in toDestroy) 
                    TryDestroyWithMarshalling(it); 
            }
        }

        #region LRU

        private int AllocateSlot()
        {
            if (_freeHead != -1)
            {
                var idx = _freeHead;
                _freeHead = _entries[idx].NextFree;
                _entries[idx].NextFree = -1;
                return idx;
            }

            if (_count < _entries.Length) return _count;

            var old = _entries;
            var next = ArrayPool<EvictablePoolEntry>.Shared.Rent(old.Length * 2);
            Array.Copy(old, next, old.Length);
            Array.Clear(next, old.Length, next.Length - old.Length);
            
            _entries = next;

            ArrayPool<EvictablePoolEntry>.Shared.Return(old, clearArray: true);
            return _count;
        }

        private T? DetachEntry(int idx)
        {
            ref var entry = ref _entries[idx];
            if (entry.Item is null) return null;
            if (_enableHashMapping) _itemToIndex!.Remove(entry.Item);

            var toDestroy = entry.Item;
            entry.Item = default;
            entry.InUse = 0;
            entry.LastUseTicks = 0;
            entry.NextFree = _freeHead;

            _freeHead = idx;

            --_count;
            _idleCount = Math.Max(0, _idleCount - 1);

            return toDestroy;
        }

        private void AddToLruTail(int idx)
        {
            ref var entry = ref _entries[idx];
            entry.Prev = _lruTail;
            entry.Next = -1;

            if (_lruTail != -1) _entries[_lruTail].Next = idx;
            _lruTail = idx;

            if (_lruHead == -1) _lruHead = idx;
        }

        private void RemoveFromLru(int idx)
        {
            ref var entry = ref _entries[idx];
            var prev = entry.Prev;
            var next = entry.Next;

            if (prev != -1) _entries[prev].Next = next;
            else _lruHead = next;

            if (next != -1) _entries[next].Prev = prev;
            else _lruTail = prev;

            entry.Prev = entry.Next = -1;
        }

        private int FindIndexByItem(T item)
        {
            if (_enableHashMapping) return _itemToIndex!.GetValueOrDefault(item, -1);

            for (var i = 0; i < _entries.Length; i++)
                if (ReferenceEquals(_entries[i].Item, item)) return i;
            
            return -1;
        }

        #endregion

        #region Destruction

        private void TryDestroyDirect(T item)
        {
            if (_onDestroy != null)
            {
                _onDestroy(item);
                return;
            }

            if (item is Object unityObject)
            {
                SafeDestroyUnityObject(unityObject);
                return;
            }

            if (item is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void TryDestroyWithMarshalling(T item)
        {
            if (_mainThreadContext == null)
            {
                TryDestroyDirect(item);
                return;
            }

            if (_onDestroy != null)
            {
                _mainThreadContext.Post(_ => _onDestroy(item), null);
                return;
            }

            if (item is Object unityObject)
            {
                _mainThreadContext.Post(_ => SafeDestroyUnityObject(unityObject), null);
                return;
            }

            if (item is IDisposable disposable)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Ex(ex);
                }
            }
        }

        private static void SafeDestroyUnityObject(Object unityObject)
        {
            try
            {
                if (!unityObject) return;
                
                switch (unityObject)
                {
                    case GameObject gameObject: 
                        Object.Destroy(gameObject); 
                        break;
                    case Component component: 
                        Object.Destroy(component.gameObject); 
                        break;
                    default: 
                        Object.Destroy(unityObject); 
                        break;
                }
            }
            catch (Exception exception) 
            { 
                Log.Ex(exception); 
            }
        }

        #endregion
    }
}