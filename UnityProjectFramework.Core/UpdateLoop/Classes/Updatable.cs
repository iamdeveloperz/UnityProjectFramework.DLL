
namespace UnityProjectFramework.Core.UpdateLoop
{
    /// <summary>
    /// Provides an abstract base class for objects that can be registered and unregistered
    /// within the update loop execution cycle. This class implements the
    /// <see cref="IManagedUpdatable"/> interface and provides methods for managing
    /// the object's lifecycle within the update system.
    /// </summary>
    /// <remarks>
    /// Classes inheriting from Updatable can participate in the update loop by calling
    /// the <see cref="BeginUpdate"/> and <see cref="EndUpdate"/> methods for registration
    /// and unregistration, respectively.
    /// </remarks>
    public abstract class Updatable : IManagedUpdatable
    {
        public void BeginUpdate() => this.RegisterUpdatable();
        public void EndUpdate() => this.UnregisterUpdatable();
    }
}