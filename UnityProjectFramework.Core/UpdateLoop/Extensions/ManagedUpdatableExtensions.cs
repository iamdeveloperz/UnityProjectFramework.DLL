
namespace UnityProjectFramework.Core.UpdateLoop
{
    public static class ManagedUpdatableExtensions
    {
        public static void RegisterUpdatable(this IManagedUpdatable updatable)
        {
            UpdateLoop.Instance.Register(updatable);
        }
        
        public static void UnregisterUpdatable(this IManagedUpdatable updatable)
        {
            UpdateLoop.Instance.Unregister(updatable);
        }
    }
}