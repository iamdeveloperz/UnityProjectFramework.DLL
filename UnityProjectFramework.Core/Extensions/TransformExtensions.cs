
using UnityEngine;

namespace UnityProjectFramework.Core
{
    /// <summary>
    /// Provides extension methods for the Transform class in Unity to enhance functionality and simplify common tasks.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Resets the transform's position, rotation, and scale to their default values.
        /// </summary>
        /// <param name="transform">The transform to reset.</param>
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Resets the local position, local rotation, and local scale of the transform to their default values.
        /// </summary>
        /// <param name="transform">The transform to reset locally.</param>
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Finds a child transform by name and returns its component of the specified type if it exists.
        /// </summary>
        /// <param name="transform">The parent transform to search within.</param>
        /// <param name="name">The name of the child transform to locate.</param>
        /// <typeparam name="T">The type of component to retrieve from the found child transform.</typeparam>
        /// <returns>
        /// The component of type T attached to the found child transform, or null if no such transform or component exists.
        /// </returns>
        public static T? Find<T>(this Transform transform, string name) where T : Component
        {
            var findTransform = transform.Find(name);
            return findTransform && findTransform.TryGetComponent(out T component) 
                ? component 
                : null;
        }

        /// <summary>
        /// Retrieves all child transforms of the specified transform.
        /// </summary>
        /// <param name="transform">The transform whose children are to be retrieved.</param>
        /// <returns>An array of child transforms the specified transform.</returns>
        public static Transform[] GetChildren(this Transform transform)
        {
            var children = new Transform[transform.childCount];
            for (var index = 0; index < transform.childCount; ++index)
            {
                children[index] = transform.GetChild(index);
            }

            return children;
        }

        /// <summary>
        /// Calculates the distance between the positions of two transforms.
        /// </summary>
        /// <param name="from">The starting transform.</param>
        /// <param name="to">The target transform.</param>
        /// <returns>The distance between the two transforms as a float.</returns>
        public static float DistanceTo(this Transform from, Transform to)
        {
            return Vector3.Distance(from.position, to.position);
        }

        /// <summary>
        /// Calculates the squared distance between the positions of two transforms.
        /// </summary>
        /// <param name="from">The starting transform for the squared distance calculation.</param>
        /// <param name="to">The target transform for the squared distance calculation.</param>
        /// <returns>The squared distance between the two transforms' positions.</returns>
        public static float SqrDistanceTo(this Transform from, Transform to)
        {
            return (from.position - to.position).sqrMagnitude;
        }
    }
}