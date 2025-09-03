
using System;
using UnityEngine;

namespace UnityProjectFramework.Core
{
    public abstract class SOAttr<T> : SO where T : Attribute
    {
        protected virtual void OnEnable()
        {
            ValidateAttributes();
        }
        
#if UNITY_EDITOR
        protected void OnValidate()
        {
            ValidateAttributes();
        }
#endif
        
        private void ValidateAttributes()
        {
            var thisType = GetType();
            var attributes = thisType.GetCustomAttributes(typeof(T), true);

            if (attributes.Length <= 0)
            {
                Debug.LogError($"SO [{name}] ({thisType.Name}) does not have {typeof(T).Name} attribute ({thisType.FullName})");
            }
        }
    }
}