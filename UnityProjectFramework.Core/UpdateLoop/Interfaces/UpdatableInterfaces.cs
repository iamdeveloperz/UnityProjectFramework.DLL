
namespace UnityProjectFramework.Core.UpdateLoop
{
    public interface IManagedUpdatable { }
    
    public interface IUpdatable : IManagedUpdatable
    {
        void OnUpdate();
    }
    
    public interface IFixedUpdatable : IManagedUpdatable
    {
        void OnFixedUpdate();
    }
    
    public interface ILateUpdatable : IManagedUpdatable
    {
        void OnLateUpdate();
    }
}