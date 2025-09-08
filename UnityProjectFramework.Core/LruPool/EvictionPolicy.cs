
namespace UnityProjectFramework.Core.LruPool
{
    /// <summary>
    /// Represents the eviction policy used by the object pool to manage
    /// the lifecycle and capacity of pooled objects.
    /// </summary>
    public enum EvictionPolicy
    {
        /// <summary>
        /// Represents an eviction policy where objects are only removed from the pool
        /// if the total number of objects exceeds the pool's maximum capacity.
        /// This policy does not consider the time-to-live (TTL) or other factors when evicting objects.
        /// </summary>
        CapacityOnly,

        /// <summary>
        /// Represents an eviction policy where objects are removed from the pool
        /// based on both the total capacity of the pool and the time-to-live (TTL)
        /// of individual objects. Objects that exceed the TTL or cause the pool
        /// to exceed its maximum capacity are eligible for removal.
        /// </summary>
        CapacityGatedTtl
    }
}