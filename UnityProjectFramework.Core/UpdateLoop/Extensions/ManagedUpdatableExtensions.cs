
namespace UnityProjectFramework.Core.UpdateLoop
{
    public static class ManagedUpdatableExtensions
    {
        public static void RegisterUpdatable(this IManagedUpdatable updatable)
        {
            UpdateLoopRunner.Instance.Register(updatable);
        }
        
        public static void UnregisterUpdatable(this IManagedUpdatable updatable)
        {
            UpdateLoopRunner.Instance.Unregister(updatable);
        }
    }
}