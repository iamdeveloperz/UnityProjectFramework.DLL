
namespace UnityProjectFramework.Core.ObjectPool;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPooling<T> where T : class
{
    /// <summary>
    /// 
    /// </summary>
    int CountAll { get; }
    /// <summary>
    /// 
    /// </summary>
    int CountActive { get; }
    /// <summary>
    /// 
    /// </summary>
    int CountInactive { get; }
    /// <summary>
    /// 
    /// </summary>
    int MaxCapacity { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    T Rent();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    PoolingObject<T> Rent(out T item);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    void Release(T element);
    /// <summary>
    /// 
    /// </summary>
    void Clear();
}