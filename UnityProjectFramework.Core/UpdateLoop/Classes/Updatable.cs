
namespace UnityProjectFramework.Core.UpdateLoop;

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
    /// <summary>
    /// Registers the instance to participate in the update execution cycle.
    /// This method allows the object to be included in the update loop (e.g., Update, FixedUpdate, LateUpdate),
    /// enabling it to perform update-related logic. Must be paired with a call to <see cref="EndUpdate"/>
    /// to unregister the instance from the update loop when updates are no longer required.
    /// </summary>
    public void BeginUpdate() => this.RegisterUpdatable();

    /// <summary>
    /// Unregisters the instance from the update execution cycle.
    /// Calling this method removes the object from the update loop (e.g., Update, FixedUpdate, LateUpdate),
    /// halting its participation in update-related logic. Should be paired with a prior call to <see cref="BeginUpdate"/>
    /// to ensure proper lifecycle management within the update system.
    /// </summary>
    public void EndUpdate() => this.UnregisterUpdatable();
}