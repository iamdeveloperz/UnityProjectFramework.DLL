
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop
{
    [AddComponentMenu("")]
    internal sealed class UpdateLoopRunner : MonoBehaviour
    {
        #region Fields

        private UpdateLoopRegistry _registry;

        #endregion
        
        public void Inject(UpdateLoopRegistry registry) => _registry = registry;
        
        #region Updates

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Update()
        {
            foreach (var updatable in _registry.Updatables)
            {
                try
                {
                    updatable.OnUpdate();
                }
                catch (Exception exception)
                {
                    Log.Ex(exception);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FixedUpdate()
        {
            foreach (var updatable in _registry.FixedUpdatables)
            {
                try
                {
                    updatable.OnFixedUpdate();
                }
                catch (Exception exception)
                {
                    Log.Ex(exception);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LateUpdate()
        {
            foreach (var updatable in _registry.LateUpdatables)
            {
                try
                {
                    updatable.OnLateUpdate();
                }
                catch (Exception exception)
                {
                    Log.Ex(exception);
                }
            }
        }

        #endregion
    }
}