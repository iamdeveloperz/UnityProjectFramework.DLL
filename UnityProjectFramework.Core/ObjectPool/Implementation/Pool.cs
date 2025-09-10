
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityProjectFramework.Core.ObjectPool;

public sealed class Pool<T> : PoolingBase<T> where T : class
{
    #region Fields & Properties
    
    private T? _freshlyReleased;
    private int _countAll;

    private readonly bool _collectionCheck;
    private readonly HashSet<T> _inactiveSet;

    public override int CountAll => _countAll;
    public override int CountInactive => Inactives.Count + (_freshlyReleased != null ? 1 : 0);

    #endregion
    
    public Pool(
        Func<T> factoryFunction, 
        Action<T> onRent, 
        Action<T> onRelease, 
        Action<T> onDestroy,
        ushort initialCapacity,
        ushort maxCapacity,
        bool collectionCheck = true) 
        : base(factoryFunction, onRent, onRelease, onDestroy, initialCapacity, maxCapacity)
    {
        _collectionCheck = collectionCheck;
        if(_collectionCheck) _inactiveSet = new HashSet<T>(new ReferenceComparer<T>());
    }

    #region Public Access

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T Rent()
    {
        T item;
        if (_freshlyReleased != null)
        {
            item = _freshlyReleased;
            _freshlyReleased = null;
            if(_collectionCheck) _inactiveSet.Remove(item);
        }
        else if (Inactives.Count == 0)
        {
            item = FactoryFunction();
            ++_countAll;
        }
        else
        {
            var index = Inactives.Count - 1;
            item = Inactives[index];
            Inactives.RemoveAt(index);
            if(_collectionCheck) _inactiveSet.Remove(item);
        }
        
        try { OnRent(item); }
        catch (Exception e) { throw new Exception($"Error while calling OnRent for {item}", e); }

        return item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Release(T element)
    {
        if (_collectionCheck)
        {
            if((_freshlyReleased != null && ReferenceEquals(_freshlyReleased, element)) || _inactiveSet.Contains(element))
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
        }
        
        try { OnRelease(element); }
        catch (Exception e) { throw new Exception($"Error while calling OnRelease for {element}", e); }

        if (_freshlyReleased == null)
        {
            _freshlyReleased = element;
            if (_collectionCheck) _inactiveSet.Add(element);
        }
        else if (CountInactive < MaxCapacityInternal)
        {
            Inactives.Add(element);
            if(_collectionCheck) _inactiveSet.Add(element);
        }
        else
        {
            --_countAll;
            DestroyElement(element);
        }
    }

    public override void Clear()
    {
        foreach (var item in Inactives) { DestroyElement(item); }
        if (_freshlyReleased != null) { DestroyElement(_freshlyReleased); }
        if (_collectionCheck) _inactiveSet.Clear();

        _freshlyReleased = null;
        Inactives.Clear();
        _countAll = 0;
    }

    #endregion

    internal bool HasElement(T element)
    {
        if (ReferenceEquals(_freshlyReleased, element)) return true;
        if (_collectionCheck) return _inactiveSet.Contains(element);
        
        for (int i = 0, n = Inactives.Count; i < n; i++)
            if (ReferenceEquals(Inactives[i], element)) return true;
        return false;
    }
}