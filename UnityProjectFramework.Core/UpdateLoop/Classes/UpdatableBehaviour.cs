
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop;

/// <summary>
/// Represents a base class for MonoBehaviour objects that want to automatically
/// register and unregister themselves to the update loop system within the framework.
/// </summary>
/// <remarks>
/// Classes deriving from this abstract class are automatically registered with
/// the framework's update loop when enabled and unregistered when disabled.
/// This enables participation in the update execution cycle via implementation of
/// a relevant update interface like <see cref="IUpdatable"/>, <see cref="IFixedUpdatable"/>,
/// or <see cref="ILateUpdatable"/>.
/// </remarks>
public abstract class UpdatableBehaviour : MonoBehaviour, IManagedUpdatable
{
    protected virtual void OnEnable() => this.RegisterUpdatable();
    protected virtual void OnDisable() => this.UnregisterUpdatable();
}