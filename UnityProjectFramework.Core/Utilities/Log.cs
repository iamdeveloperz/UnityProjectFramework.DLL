
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UnityProjectFramework.Core
{
    /// <summary>
    /// Provides static methods for logging messages in different debug levels.
    /// Need System.Diagnostics.Conditional to work.
    /// Pdv.SYMBOL_LOG_ENABLED must be defined to use this class.
    /// </summary>
    public static class Log
    {
        #region Const & Struct

        private const string INFO_COLOR = "<color=#56F8FF>";
        private const string WARNING_COLOR = "<color=#FFFD56>";
        private const string ERROR_COLOR = "<color=#FF6756>";
        private const string COLOR_END = "</color>";
        
        public enum DebugLevel
        {
            Info,
            Warning,
            Error,
            Exception
        }

        #endregion

        public static DebugLevel Level { get; private set; } = DebugLevel.Error;

        /// <summary>
        /// Sets the logging level for the application.
        /// </summary>
        /// <param name="level">The desired debug level to set. This determines the minimum severity of messages to be logged.</param>
        public static void SetLevel(DebugLevel level)
        {
            Level = level;
        }

        #region Level #Debug

        /// <summary>
        /// Logs a message at the Info debug level.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(object message)
        {
            if (Level >= DebugLevel.Info)
            {
                Debug.Log($"{INFO_COLOR}[INFO]{COLOR_END} {message}");
            }
        }

        /// <summary>
        /// Logs a message at the Info debug level with optional color formatting.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="hexColor">The hex color code used to style the message.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(object message, string hexColor)
        {
            if (Level >= DebugLevel.Info)
            {
                Debug.Log($"{INFO_COLOR}[INFO]{COLOR_END} <color={hexColor}>{message}</color>");
            }
        }

        /// <summary>
        /// Logs a formatted message at the Info debug level.
        /// </summary>
        /// <param name="formattedMessage">The formatted message to be logged, which may include placeholders for arguments.</param>
        /// <param name="args">An array of objects to format into the message placeholders.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void D(string formattedMessage, params object[] args)
        {
            if (Level >= DebugLevel.Info)
            {
                Debug.LogFormat($"{INFO_COLOR}[INFO]{COLOR_END} {formattedMessage}", args);
            }
        }

        #endregion

        #region Level #Warning

        /// <summary>
        /// Logs a warning message if the current debug level allows warnings to be logged.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void W(object message)
        {
            if(Level >= DebugLevel.Warning)
            {
                Debug.LogWarning($"{WARNING_COLOR}[INFO]{COLOR_END} {message}");
            }
        }

        /// <summary>
        /// Logs a message at the Warning debug level with formatted content.
        /// </summary>
        /// <param name="formattedMessage">The formatted message to log.</param>
        /// <param name="args">An array of objects to format into the formatted message.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void W(string formattedMessage, params object[] args)
        {
            if(Level >= DebugLevel.Warning)
            {
                Debug.LogWarningFormat($"{WARNING_COLOR}[INFO]{COLOR_END} {formattedMessage}", args);
            }
        }

        #endregion

        #region Level #Error & Exception

        /// <summary>
        /// Logs an error level message to the console.
        /// </summary>
        /// <param name="message">The message to be logged as an error.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void E(object message)
        {
            if (Level >= DebugLevel.Error)
            {
                Debug.LogError($"{ERROR_COLOR}[INFO]{COLOR_END} {message}");
            }
        }

        /// <summary>
        /// Logs an error message with optional formatted arguments.
        /// </summary>
        /// <param name="formattedMessage">The error message that can contain format items.</param>
        /// <param name="args">An array of objects to format and include in the error message.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void E(string formattedMessage, params object[] args)
        {
            if (Level >= DebugLevel.Error)
            {
                Debug.LogErrorFormat($"{ERROR_COLOR}[INFO]{COLOR_END} {formattedMessage}", args);
            }
        }

        /// <summary>
        /// Logs an exception if the current logging level includes exceptions.
        /// </summary>
        /// <param name="exception">The exception to log. Contains detailed information about the error that occurred.</param>
        [Conditional(Pdv.SYMBOL_LOG_ENABLED)]
        public static void Ex(Exception exception)
        {
            if (Level >= DebugLevel.Exception)
            {
                Debug.LogException(exception);
            }
        }

        #endregion
    }
}