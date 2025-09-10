namespace UnityProjectFramework.Core.LruPool
{
    public interface IEvictablePool<T>
    {
        int Count { get; }
        int Capacity { get; }
        
        T Rent();
        void Return(T item);
        void SetTtlMilliseconds(int ttlMs);
        void SetMaxCapacity(int maxCapacity);
    }
}