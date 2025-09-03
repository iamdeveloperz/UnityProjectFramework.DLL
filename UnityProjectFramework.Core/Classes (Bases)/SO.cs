
using UnityEngine;

namespace UnityProjectFramework.Core
{
    public abstract class SO : ScriptableObject
    {
#pragma warning disable CS0169
        [SerializeField] [TextArea]
        private string _description = "Input your comments or description here.";
    }
}