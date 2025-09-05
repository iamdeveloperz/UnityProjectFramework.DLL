
using System;
using UnityEngine;

namespace UnityProjectFramework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Pdv
    {
        #region Symbols
        
        public const string SYMBOL_LOG_ENABLED = "LOG_ENABLED";

        #endregion

        #region Float

        // === Angles (Degrees) ===
        public const float Angle0 = 0f;
        public const float Angle15 = 15f;
        public const float Angle30 = 30f;
        public const float Angle45 = 45f;
        public const float Angle60 = 60f;
        public const float Angle75 = 75f;
        public const float Angle90 = 90f;
        public const float Angle105 = 105f;
        public const float Angle120 = 120f;
        public const float Angle135 = 135f;
        public const float Angle150 = 150f;
        public const float Angle165 = 165f;
        public const float Angle180 = 180f;
        public const float Angle195 = 195f;
        public const float Angle210 = 210f;
        public const float Angle225 = 225f;
        public const float Angle240 = 240f;
        public const float Angle255 = 255f;
        public const float Angle270 = 270f;
        public const float Angle285 = 285f;
        public const float Angle300 = 300f;
        public const float Angle315 = 315f;
        public const float Angle330 = 330f;
        public const float Angle345 = 345f;
        public const float Angle360 = 360f;
        
        // === Radians ===
        public const float Rad0 = Angle0;
        public const float Rad15 = Angle15 * Mathf.Deg2Rad;
        public const float Rad30 = Angle30 * Mathf.Deg2Rad;
        public const float Rad45 = Angle45 * Mathf.Deg2Rad;
        public const float Rad60 = Angle60 * Mathf.Deg2Rad;
        public const float Rad75 = Angle75 * Mathf.Deg2Rad;
        public const float Rad90 = Angle90 * Mathf.Deg2Rad;
        public const float Rad105 = Angle105 * Mathf.Deg2Rad;
        public const float Rad120 = Angle120 * Mathf.Deg2Rad;
        public const float Rad135 = Angle135 * Mathf.Deg2Rad;
        public const float Rad150 = Angle150 * Mathf.Deg2Rad;
        public const float Rad165 = Angle165 * Mathf.Deg2Rad;
        public const float Rad180 = Angle180 * Mathf.Deg2Rad;
        public const float Rad195 = Angle195 * Mathf.Deg2Rad;
        public const float Rad210 = Angle210 * Mathf.Deg2Rad;
        public const float Rad225 = Angle225 * Mathf.Deg2Rad;
        public const float Rad240 = Angle240 * Mathf.Deg2Rad;
        public const float Rad255 = Angle255 * Mathf.Deg2Rad;
        public const float Rad270 = Angle270 * Mathf.Deg2Rad;
        public const float Rad285 = Angle285 * Mathf.Deg2Rad;
        public const float Rad300 = Angle300 * Mathf.Deg2Rad;
        public const float Rad315 = Angle315 * Mathf.Deg2Rad;
        public const float Rad330 = Angle330 * Mathf.Deg2Rad;
        public const float Rad345 = Angle345 * Mathf.Deg2Rad;
        public const float Rad360 = Angle360 * Mathf.Deg2Rad;
        
        // === Time Constants ===
        public const float SecPerMin = 60f;
        public const float SecPerHour = 3600f;
        public const float SecPerDay = 86400f;
        public const float SecPerWeek = 604800f;
        public const float MinPerHour = 60f;
        public const float MinPerDay = 1440f;
        public const float MinPerWeek = 10080f;
        public const float HourPerDay = 24f;
        public const float HourPerWeek = 168f;
        
        // === Float ===
        public const float Epsilon = MathF.E;

        #endregion
    }
}