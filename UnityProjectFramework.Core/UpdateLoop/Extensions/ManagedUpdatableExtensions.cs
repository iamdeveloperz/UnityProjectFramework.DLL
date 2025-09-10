
namespace UnityProjectFramework.Core.UpdateLoop;

/// <summary>
/// Provides extension methods for objects implementing the IManagedUpdatable interface.
/// These methods enable simplified registration and unregistration
/// of updatable objects to the update loop framework.
/// </summary>
public static class ManagedUpdatableExtensions
{
    /// <summary>
    /// Registers an object implementing the <see cref="IManagedUpdatable"/> interface
    /// with the update loop runner. This enables the object to be included in the
    /// update execution cycle (e.g., Update, FixedUpdate, LateUpdate).
    /// </summary>
    /// <param name="updatable">The object to be registered. Must implement <see cref="IManagedUpdatable"/>.</param>
    public static void RegisterUpdatable(this IManagedUpdatable updatable)
    {
        UpdateLoopRunner.Instance.Register(updatable);
    }

    /// <summary>
    /// Unregisters an object implementing the <see cref="IManagedUpdatable"/> interface
    /// from the update loop runner. This removes the object from participating in
    /// the update execution cycle (e.g., Update, FixedUpdate, LateUpdate).
    /// </summary>
    /// <param name="updatable">The object to be unregistered. Must implement <see cref="IManagedUpdatable"/>.</param>
    public static void UnregisterUpdatable(this IManagedUpdatable updatable)
    {
        UpdateLoopRunner.Instance.Unregister(updatable);
    }
}