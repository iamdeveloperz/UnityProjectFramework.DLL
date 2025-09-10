
using System;

namespace UnityProjectFramework.Core.ObjectPool;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct PoolingObject<T> : IDisposable where T : class
{
    private readonly T _toReturn;
    private readonly IPooling<T> _pool;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pool"></param>
    public PoolingObject(T value, IPooling<T> pool)
    {
        _toReturn = value;
        _pool = pool;
    }
        
    void IDisposable.Dispose() => _pool.Release(_toReturn);
}