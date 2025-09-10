
namespace UnityProjectFramework.Core.UpdateLoop;

/// <summary>
/// Represents a marker interface for objects that are managed within the update loop framework.
/// Classes implementing this interface can be registered for update functionality,
/// allowing them to participate in one or more of the update cycles (e.g., Update, FixedUpdate, LateUpdate).
/// </summary>
public interface IManagedUpdatable { }
    
public interface IUpdatable : IManagedUpdatable
{
    /// <summary>
    /// Executes logic that is intended to be called during each frame's update cycle.
    /// This method should encapsulate behavior that needs to be consistently executed
    /// at each frame of the game or application, typically for time-sensitive operations.
    /// Classes implementing this should define precise actions required during updates.
    /// </summary>
    void OnUpdate();
}
    
public interface IFixedUpdatable : IManagedUpdatable
{
    /// <summary>
    /// Defines the behavior to be executed during Unity's FixedUpdate cycle.
    /// This method should contain logic that operates at fixed time intervals, typically used for
    /// physics-related updates or deterministic systems reliant on consistent timing.
    /// Classes implementing this method should ensure thread safety and account for its
    /// integration within the managed update loop framework.
    /// </summary>
    void OnFixedUpdate();
}
    
public interface ILateUpdatable : IManagedUpdatable
{
    /// <summary>
    /// Executes logic specific to the LateUpdate phase of the Unity update loop.
    /// This method is invoked after the Update and FixedUpdate cycles, allowing for
    /// operations that require completion of other update processes, such as post-processing or final adjustments.
    /// Classes implementing this method should define behavior that is essential
    /// during this phase of the game or application's execution lifecycle.
    /// </summary>
    void OnLateUpdate();
}