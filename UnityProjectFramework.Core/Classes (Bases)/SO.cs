
using UnityEngine;

namespace UnityProjectFramework.Core;

/// <summary>
/// Represents an abstract base class for ScriptableObject types within the Unity Project Framework.
/// This class provides a foundation for defining ScriptableObject assets with custom logic or behaviors
/// and can be extended to include additional functionality specific to the application.
/// </summary>
public abstract class SO : ScriptableObject
{
#pragma warning disable CS0169
    [SerializeField] [TextArea]
    private string _description = "Input your comments or description here.";
}