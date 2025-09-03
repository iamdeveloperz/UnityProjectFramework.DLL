
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop
{
    internal static class UpdateLoopEntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (!UpdateLoopRunner.Instance)
            {
                UpdateLoopRunner.CreateInstance();
            }
        }
    }
}