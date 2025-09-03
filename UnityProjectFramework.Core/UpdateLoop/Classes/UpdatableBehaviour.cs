
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop
{
    public abstract class UpdatableBehaviour : MonoBehaviour, IManagedUpdatable
    {
        protected virtual void OnEnable() => this.RegisterUpdatable();
        protected virtual void OnDisable() => this.UnregisterUpdatable();
    }
}