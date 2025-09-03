
using UnityEngine;

namespace UnityProjectFramework.Core
{
    public static class ApplicationUtility
    {
        public static bool IsQuit { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            IsQuit = false;
            Application.quitting += OnApplicationQuit;
        }
        
        private static void OnApplicationQuit()
        {
            IsQuit = true;
            Application.quitting -= OnApplicationQuit;
        }
    }
}