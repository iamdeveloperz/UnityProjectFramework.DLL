using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace UnityProjectFramework.Core.LruPool
{
    public sealed partial class LruPool<T> : IEvictablePool<T>, IDisposable where T : class
    {
        #region Fields

        private EvictablePoolEntry[] _entries;
        private readonly Dictionary<T, int>? _itemToIndex;

        private int _count;
        private int _idleCount;
        private int _freeHead = -1, _lruHead = -1, _lruTail = -1;
        
        private readonly Func<T> _factoryFunction;
        private readonly Action<T> _onRent;
        private readonly Action<T> _onReturn;
        private readonly Action<T>? _onDestroy;

        private int _capacity;
        private volatile int _ttlMs;
        private readonly EvictionPolicy _policy;
        private readonly bool _enableHashMapping;

        private readonly double _tickToMs = 1000.0 / Stopwatch.Frequency;
        private readonly object _gate = new();
        private volatile bool _isDisposed;
        
        private readonly Timer _backgroundTimer;
        private readonly int _scanPeriodMs;
        private readonly SynchronizationContext? _mainThreadContext;

        #endregion

        #region Ctor / Dispose / Props

        public int Capacity => _capacity;
        public int Count { get { lock (_gate) return _count; }}
        public int IdleCount { get { lock (_gate) return _idleCount; }}

        public LruPool(ObjectPoolParameters parameters,
            Func<T> factoryFunction,
            Action<T> onRent,
            Action<T> onReturn,
            Action<T>? onDestroy = null)
        {
            _factoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
            _onRent = onRent ?? throw new ArgumentNullException(nameof(onRent));
            _onReturn = onReturn ?? throw new ArgumentNullException(nameof(onReturn));
            _onDestroy = onDestroy;

            _capacity = Math.Max(1, parameters.MaxCapacity);
            _scanPeriodMs = parameters.ScanPeriodMs;
            _ttlMs = parameters.TimeToLiveMs;
            _policy = parameters.EvictionPolicy;
            _enableHashMapping = parameters.EnableHashMapping;
            
            _mainThreadContext = SynchronizationContext.Current;
            _entries = ArrayPool<EvictablePoolEntry>.Shared.Rent(Math.Max(4, parameters.InitialCapacity));
            
            Array.Clear(_entries, 0, _entries.Length);

            if (_enableHashMapping)
            {
                _itemToIndex = new Dictionary<T, int>(Math.Max(4, parameters.InitialCapacity), new ReferenceComparer<T>());
            }
            
            _backgroundTimer = new Timer(ScanTtlEntry, null, _scanPeriodMs, _scanPeriodMs);
        }

        public void Dispose()
        {
            _isDisposed = true;
            _backgroundTimer?.Dispose();

            List<T>? toDestroy = null;
            lock (_gate)
            {
                for (var i = 0; i < _entries.Length; ++i)
                {
                    if (_entries[i].Item is not null)
                    {
                        (toDestroy ??= new List<T>()).Add(_entries[i].Item);
                        _entries[i].Item = default;
                    }
                }
                
                _itemToIndex?.Clear();
                ArrayPool<EvictablePoolEntry>.Shared.Return(_entries, clearArray: true);
                _entries = Array.Empty<EvictablePoolEntry>();
                _count = _idleCount = 0;
                _freeHead = _lruHead = _lruTail = -1;
            }

            if (toDestroy != null)
            {
                foreach (var it in toDestroy) 
                    TryDestroyDirect(it);
            }
        }

        #endregion

        #region Public API

        public T Rent()
        {
            T item;
            lock (_gate)
            {
                if (_lruTail != -1)
                {
                    var idx = _lruTail;
                    RemoveFromLru(idx);
                    ref var e = ref _entries[idx];
                    e.InUse = 1;
                    e.LastUseTicks = Stopwatch.GetTimestamp();
                    --_idleCount;
                    if (_enableHashMapping) _itemToIndex![e.Item] = idx;
                    item = e.Item;
                }
                else
                {
                    var slot = AllocateSlot();
                    ref var e = ref _entries[slot];
                    e.Item = _factoryFunction();
                    e.InUse = 1;
                    e.LastUseTicks = Stopwatch.GetTimestamp();
                    ++_count;
                    if (_enableHashMapping) _itemToIndex![e.Item] = slot;
                    item = e.Item;
                }
            }
            _onRent?.Invoke(item);
            return item;
        }

        public void Return(T item)
        {
            if (item == null) return;

            lock (_gate)
            {
                var idx = FindIndexByItem(item);
                if (idx < 0) return;
                ref var e = ref _entries[idx];
                if (e.InUse == 0) return;

                e.InUse = 0;
                e.LastUseTicks = Stopwatch.GetTimestamp();
                AddToLruTail(idx);
                _idleCount++;
            }

            _onReturn?.Invoke(item);
        }

        public void SetTtlMilliseconds(int ttlMs) => _ttlMs = Math.Max(0, ttlMs);

        public void SetMaxCapacity(int maxCapacity)
        {
            lock (_gate)
            {
                _capacity = Math.Max(1, maxCapacity);
                ApplyEvictionOnReturn_NoUnityCalls();
            }
        }

        public void TrimExcess()
        {
            List<T>? toDestroy = null;
            lock (_gate)
            {
                while (_idleCount > 0 && _count > _capacity)
                {
                    var idx = _lruHead;
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

        #endregion
    }
}