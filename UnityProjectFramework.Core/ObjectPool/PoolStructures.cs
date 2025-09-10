
namespace UnityProjectFramework.Core.LruPool
{
    /// <summary>
    /// 
    /// </summary>
    public struct ObjectPoolParameters
    {
        /// <summary>
        /// 
        /// </summary>
        public int InitialCapacity;
        
        /// <summary>
        /// 
        /// </summary>
        public int MaxCapacity;
        
        /// <summary>
        /// 
        /// </summary>
        public EvictionPolicy EvictionPolicy;
        
        /// <summary>
        /// 
        /// </summary>
        public int TimeToLiveMs;
        
        /// <summary>
        /// 
        /// </summary>
        public int ScanPeriodMs;
        
        /// <summary>
        /// 
        /// </summary>
        public bool EnableHashMapping;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialCapacity"></param>
        /// <param name="maxCapacity"></param>
        /// <param name="evictionPolicy"></param>
        /// <param name="timeToLiveMs"></param>
        /// <param name="scanPeriodMs"></param>
        /// <param name="enableHashMapping"></param>
        public ObjectPoolParameters(int initialCapacity, 
            int maxCapacity, 
            EvictionPolicy evictionPolicy, 
            int timeToLiveMs,
            int scanPeriodMs, 
            bool enableHashMapping)
        {
            InitialCapacity = initialCapacity;
            MaxCapacity = maxCapacity;
            EvictionPolicy = evictionPolicy;
            TimeToLiveMs = timeToLiveMs;
            ScanPeriodMs = scanPeriodMs;
            EnableHashMapping = enableHashMapping;
        }

        /// <summary>
        /// 
        /// </summary>
        public static ObjectPoolParameters Default => new ObjectPoolParameters
        {
            InitialCapacity = 16,
            MaxCapacity = 128,
            EvictionPolicy = EvictionPolicy.CapacityGatedTtl,
            TimeToLiveMs = 30_000,
            ScanPeriodMs = 1_000,
            EnableHashMapping = true,
        };
    }
}