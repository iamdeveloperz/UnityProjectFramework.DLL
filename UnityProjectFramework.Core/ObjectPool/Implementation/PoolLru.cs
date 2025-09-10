
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityProjectFramework.Core.ObjectPool;

public sealed class PoolLru<T> : PoolingBase<T> where T : class
{
    private struct LruItem
    {
        public T Item;
        
    }
    
    #region Fields & Properties
    
    private readonly LruPolicy _policy;

    private readonly double _tickToMs = 1000.0 / Stopwatch.Frequency;
    private readonly object _gate = new();
    private readonly Timer _scanTimer;
    private readonly SynchronizationContext _mainThreadContext;
    private readonly int _scanPeriodMs;
    private volatile int _ttlMs;

    private int _countAll;
    
    public override int CountAll => _countAll;
    public override int CountInactive { get; }

    #endregion

    public PoolLru(
        Func<T> factoryFunction, 
        Action<T> onRent, 
        Action<T> onRelease, 
        Action<T> onDestroy,
        LruPolicy policy,
        int ttlMs,
        int scanPeriodMs,
        int initialCapacity,
        int maxCapacity) 
        : base(factoryFunction, onRent, onRelease, onDestroy, initialCapacity, maxCapacity)
    {
        _policy = policy;
        _ttlMs = ttlMs;
        _scanPeriodMs = scanPeriodMs;

        _mainThreadContext = SynchronizationContext.Current;
        _scanTimer = new Timer(ScanTtlFromPool, null, _scanPeriodMs, _scanPeriodMs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T Rent()
    {
        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Release(T element)
    {
        
    }

    public override void Clear()
    {
        
    }
    
    public void SetTtlMilliseconds(int ttlMs)
    {
        _ttlMs = Math.Max(0, ttlMs);
    }

    #region Private

    private void ScanTtlFromPool(object? _)
    {
        
    }

    #endregion
}