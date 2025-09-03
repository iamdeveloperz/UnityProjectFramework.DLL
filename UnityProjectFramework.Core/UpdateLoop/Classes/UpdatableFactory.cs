
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop
{
    /// <summary>
    /// Internal factory for creating update loop runners.
    /// Manages the creation and configuration of both system and custom runners.
    /// </summary>
    internal sealed class UpdatableFactory
    {
        #region Constants

        private const string RUNNER_NAME = "[UpdateLoop.System]";
        private const HideFlags RunnerHideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;

        #endregion

        #region Runner Creation

        /// <summary>
        /// Create the persistent runner that cannot be paused.
        /// </summary>
        internal UpdateLoopRunner CreateRunner(UpdateLoopRegistry registry)
        {
            var runnerObject = new GameObject(RUNNER_NAME)
            {
                hideFlags = RunnerHideFlags
            };

            Object.DontDestroyOnLoad(runnerObject);

            var runner = runnerObject.AddComponent<UpdateLoopRunner>();
            runner.Inject(registry);
            Log.D($"[UpdateLoop] System runner created (non-pausable)");
            
            return runner;
        }

        #endregion
    }
}