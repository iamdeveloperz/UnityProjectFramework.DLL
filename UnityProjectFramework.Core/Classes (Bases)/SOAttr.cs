
using System;
using UnityEngine;

namespace UnityProjectFramework.Core;

/// <summary>
/// Represents an abstract base class for ScriptableObject types that validate the presence of a specified attribute.
/// Inherits from the SO base class and is restricted to attributes of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the attribute that the ScriptableObject must have. Must be derived from <see cref="System.Attribute"/>.</typeparam>
/// <remarks>
/// This class ensures that ScriptableObject-derived classes are decorated with the specified attribute <typeparamref name="T"/>.
/// If the attribute is missing, an error is logged during runtime.
/// </remarks>
/// <example>
/// This class can be used to enforce specific metadata requirements for ScriptableObject implementations in Unity.
/// </example>
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