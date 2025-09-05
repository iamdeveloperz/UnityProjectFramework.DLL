
using UnityEngine;

namespace UnityProjectFramework.Core
{
    /// <summary>
    /// Provides extension methods for Unity's GameObject class.
    /// These methods simplify common operations on GameObjects
    /// such as adding, removing, or checking for components,
    /// manipulating their layers, and handling their activation or destruction safely.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Retrieves the component of type <typeparamref name="T"/> if it exists on the GameObject.
        /// If the component does not exist, it will be added to the GameObject and returned.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve or add.</typeparam>
        /// <param name="gameObject">The GameObject on which to retrieve or add the component.</param>
        /// <returns>The existing or newly added component of type <typeparamref name="T"/>.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T component) ? component : gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Attempts to remove the component of type <typeparamref name="T"/> from the GameObject.
        /// If the component exists, it is destroyed and the method returns <c>true</c>.
        /// If the component does not exist, the method returns <c>false</c>.
        /// </summary>
        /// <typeparam name="T">The type of the component to remove.</typeparam>
        /// <param name="gameObject">The GameObject from which the component will be removed.</param>
        /// <returns><c>true</c> if the component was found and removed; otherwise, <c>false</c>.</returns>
        public static bool TryRemoveComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent(out T component)) return false;
            
            Object.Destroy(component);
            return true;
        }

        /// <summary>
        /// Determines whether the GameObject has a component of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the component to check for.</typeparam>
        /// <param name="gameObject">The GameObject to check for the component.</param>
        /// <returns>True if the component of type <typeparamref name="T"/> exists on the GameObject; otherwise, false.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T _);
        }

        /// <summary>
        /// Safely sets the active state of the specified GameObject. Ensures the operation is only performed if the GameObject is not null.
        /// </summary>
        /// <param name="gameObject">The GameObject whose active state is to be set.</param>
        /// <param name="isActive">The desired active state of the GameObject. True for active, false for inactive.</param>
        public static void SafeSetActive(this GameObject gameObject, bool isActive)
        {
            if (!gameObject) return;

            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Sets the layer of the GameObject and all of its child GameObjects recursively to the specified layer.
        /// </summary>
        /// <param name="gameObject">The GameObject whose layer and child layers will be modified.</param>
        /// <param name="layer">The layer to set for the GameObject and its children.</param>
        public static void SetLayerChildren(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            for (var index = 0; index < gameObject.transform.childCount; ++index)
            {
                var child = gameObject.transform.GetChild(index);
                child?.gameObject.SetLayerChildren(layer);
            }
        }

        /// <summary>
        /// Sets the layer of the GameObject and all its children to the specified layer by layer name.
        /// </summary>
        /// <param name="gameObject">The GameObject whose layer and children's layers will be set.</param>
        /// <param name="layerName">The name of the layer to set for the GameObject and its children.</param>
        public static void SetLayerChildren(this GameObject gameObject, string layerName)
        {
            gameObject.layer = LayerMask.NameToLayer(layerName);
            for (var index = 0; index < gameObject.transform.childCount; ++index)
            {
                var child = gameObject.transform.GetChild(index);
                child?.gameObject.SetLayerChildren(layerName);
            }
        }

        /// <summary>
        /// Destroys all child GameObjects of the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject whose child GameObjects will be destroyed.</param>
        public static void DestroyChildren(this GameObject gameObject)
        {
            for (var index = gameObject.transform.childCount - 1; index >= 0; --index)
            {
                var child = gameObject.transform.GetChild(index);
                Object.Destroy(child?.gameObject);
            }
        }

        /// <summary>
        /// Safely destroys the specified GameObject, ensuring the appropriate destruction
        /// handling depending on whether the application is in play mode or edit mode.
        /// </summary>
        /// <param name="gameObject">The GameObject to be destroyed.</param>
        public static void SafeDestroy(this GameObject gameObject)
        {
            if (!gameObject) return;
            
            if(Application.isPlaying) Object.Destroy(gameObject);
            else Object.DestroyImmediate(gameObject);
        }
    }
}