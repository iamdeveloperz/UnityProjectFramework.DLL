
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace UnityProjectFramework.Core
{
    public static class Log
    {
        public enum LoggingLevel
        {
            Info,
            Warning,
            Error,
            Exception
        }

        public static LoggingLevel Level { get; private set; } = LoggingLevel.Error;
        public static void SetLevel(LoggingLevel level)
        {
            Level = level;
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(string message)
        {
            if (Level >= LoggingLevel.Info)
            {
                Debug.Log($"");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(string message, string hexColor)
        {
            if (Level >= LoggingLevel.Info)
            {
                Debug.Log($"");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(string formattedMessage, params object[] args)
        {
            if (Level >= LoggingLevel.Info)
            {
                Debug.Log($"");
            }
        }

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void W(string message)
        {
            if(Level >= LoggingLevel.Warning)
            {
                Debug.LogWarning($"");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void W(string formattedMessage, params object[] args)
        {
            if(Level >= LoggingLevel.Warning)
            {
                Debug.LogWarning($"");
            }
        }

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void E()
        {
            
        }

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void Ex()
        {
            
        }
    }
}