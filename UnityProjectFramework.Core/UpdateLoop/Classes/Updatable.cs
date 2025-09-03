
namespace UnityProjectFramework.Core.UpdateLoop
{
    public abstract class Updatable : IManagedUpdatable
    {
        public void BeginUpdate() => this.RegisterUpdatable();
        public void EndUpdate() => this.UnregisterUpdatable();
    }
}