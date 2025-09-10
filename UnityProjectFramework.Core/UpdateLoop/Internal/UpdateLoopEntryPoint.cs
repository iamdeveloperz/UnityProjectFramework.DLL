
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop;

/// <summary>
/// Responsible for providing an entry point for the update loop mechanics
/// within the Unity Project Framework Core. This class is intended to
/// be used internally within the framework to facilitate operations
/// tied to Unity's runtime update cycle.
/// </summary>
internal static class UpdateLoopEntryPoint
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void WarmUp()
    {
        if (!UpdateLoopRunner.Instance)
        {
            UpdateLoopRunner.CreateInstance();
        }
    }
}