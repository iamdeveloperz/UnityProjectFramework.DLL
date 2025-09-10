
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace UnityProjectFramework.Core.ObjectPool;

public abstract class PoolingBase<T> : IDisposable, IPooling<T> where T : class
{
    #region Fields & Properties

    protected List<T> Inactives;

    protected readonly Func<T> FactoryFunction;
    protected readonly Action<T> OnRent;
    protected readonly Action<T> OnRelease;
    protected readonly Action<T> OnDestroy;

    protected int MaxCapacityInternal;
    protected volatile bool IsDisposed;
    
    public int MaxCapacity => MaxCapacityInternal;
    public int CountActive => CountAll - CountInactive;
    public abstract int CountAll { get; }
    public abstract int CountInactive { get; }

    #endregion
    
    protected PoolingBase(
        Func<T> factoryFunction,
        Action<T> onRent,
        Action<T> onRelease,
        Action<T> onDestroy,
        int defaultCapacity,
        int maxCapacity)
    {
        FactoryFunction = factoryFunction ?? throw new ArgumentNullException(nameof(factoryFunction));
        if (maxCapacity <= 0) throw new ArgumentException("Capacity must be greater than 0", nameof(maxCapacity));
        
        OnRent = onRent;
        OnRelease = onRelease;
        OnDestroy = onDestroy;
        MaxCapacityInternal = maxCapacity;
        
        Inactives = new List<T>(Math.Min(defaultCapacity, MaxCapacityInternal));
    }
    
    public void Dispose()
    {
        if (IsDisposed) return;

        IsDisposed = true;
        Clear();
    }

    #region Abstract Methods

    public abstract T Rent();
    public abstract void Release(T element);
    public abstract void Clear();

    #endregion

    public PoolingObject<T> Rent(out T item)
    {
        item = Rent();
        return new PoolingObject<T>(item, this);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void DestroyElement(T element)
    {
        if (OnDestroy != null)
        {
            try { OnDestroy(element); }
            catch (Exception ex) { Log.Ex(ex); }
            return;
        }

        switch (element)
        {
            case Object unityObject:
                Object.Destroy(unityObject);
                break;
            case IDisposable disposable:
                disposable.Dispose();
                break;
            default:
                Log.E($"Element is not a Unity Object or IDisposable. Type: {element.GetType().Name}");
                break;
        }
    }
}