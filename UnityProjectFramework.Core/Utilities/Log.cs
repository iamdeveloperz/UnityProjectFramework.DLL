
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace UnityProjectFramework.Core
{
    public static class Log
    {
        #region Const & Struct

        private const string INFO_COLOR = "<color=#56F8FF>";
        private const string WARNING_COLOR = "<color=#FFFD56>";
        private const string ERROR_COLOR = "<color=#FF6756>";
        private const string COLOR_END = "</color>";
        
        public enum LoggingLevel
        {
            Info,
            Warning,
            Error,
            Exception
        }

        #endregion

        public static LoggingLevel Level { get; private set; } = LoggingLevel.Error;
        public static void SetLevel(LoggingLevel level)
        {
            Level = level;
        }

        #region Level #Debug

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(object message)
        {
            if (Level >= LoggingLevel.Info)
            {
                Debug.Log($"{INFO_COLOR}[INFO]{COLOR_END} {message}");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(object message, string hexColor)
        {
            if (Level >= LoggingLevel.Info)
            {
                Debug.Log($"{INFO_COLOR}[INFO]{COLOR_END} <color={hexColor}>{message}</color>");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(string formattedMessage, params object[] args)
        {
            if (Level >= LoggingLevel.Info)
            {
                Debug.LogFormat($"{INFO_COLOR}[INFO]{COLOR_END} {formattedMessage}", args);
            }
        }

        #endregion

        #region Level #Warning

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void W(object message)
        {
            if(Level >= LoggingLevel.Warning)
            {
                Debug.LogWarning($"{WARNING_COLOR}[INFO]{COLOR_END} {message}");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void W(string formattedMessage, params object[] args)
        {
            if(Level >= LoggingLevel.Warning)
            {
                Debug.LogWarningFormat($"{WARNING_COLOR}[INFO]{COLOR_END} {formattedMessage}", args);
            }
        }

        #endregion

        #region Level #Error & Exception

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void E(object message)
        {
            if (Level >= LoggingLevel.Error)
            {
                Debug.LogError($"{ERROR_COLOR}[INFO]{COLOR_END} {message}");
            }
        }
        
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void E(string formattedMessage, params object[] args)
        {
            if (Level >= LoggingLevel.Error)
            {
                Debug.LogErrorFormat($"{ERROR_COLOR}[INFO]{COLOR_END} {formattedMessage}", args);
            }
        }

        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void Ex(Exception exception)
        {
            if (Level >= LoggingLevel.Exception)
            {
                Debug.LogException(exception);
            }
        }

        #endregion
    }
}