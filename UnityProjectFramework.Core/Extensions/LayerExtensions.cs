using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProjectFramework.Core;

/// <summary>
/// Defines extension methods for working with Unity LayerMask, GameObject, and Component layers.
/// Includes functionality for layer validation, manipulation, combination, intersection, exclusion,
/// layer conversion, and recursive layer modifications on GameObject hierarchies.
/// </summary>
public static class LayerExtensions
{
    private const int MIN_LAYER = 0;
    private const int MAX_LAYER = 31;

    #region Layer Mask

    /// <summary>
    /// Determines whether a specified layer is contained within a LayerMask.
    /// </summary>
    /// <param name="mask">The LayerMask to check.</param>
    /// <param name="layer">The layer index to validate and check for inclusion in the mask.</param>
    /// <returns>True if the specified layer is included in the LayerMask; otherwise, false.</returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return layer >= MIN_LAYER && layer <= MAX_LAYER && (mask.value & (1 << layer)) != 0;
    }

    /// <summary>
    /// Determines whether a specific layer specified by its name is contained within a LayerMask.
    /// </summary>
    /// <param name="mask">The LayerMask to check.</param>
    /// <param name="layerName">The name of the layer to validate and check for inclusion in the mask.</param>
    /// <returns>True if the layer identified by the given name is included in the LayerMask; otherwise, false.</returns>
    public static bool Contains(this LayerMask mask, string layerName)
    {
        return TryGetLayer(layerName, out var layer) && mask.Contains(layer);
    }

    /// <summary>
    /// Adds a specified layer to the LayerMask.
    /// </summary>
    /// <param name="mask">The LayerMask to which the layer will be added.</param>
    /// <param name="layer">The layer index to add to the mask.</param>
    /// <returns>A new LayerMask with the specified layer added. If the layer is invalid, the original LayerMask is returned.</returns>
    public static LayerMask Add(this LayerMask mask, int layer)
    {
        return layer < MIN_LAYER || layer > MAX_LAYER ? mask : (LayerMask)(mask | (1 << layer));
    }

    /// <summary>
    /// Adds a specified layer to a LayerMask using the layer name.
    /// </summary>
    /// <param name="mask">The LayerMask to which the layer will be added.</param>
    /// <param name="layerName">The name of the layer to add.</param>
    /// <returns>A new LayerMask including the specified layer if it exists, or the original LayerMask if the layer does not exist.</returns>
    public static LayerMask Add(this LayerMask mask, string layerName)
    {
        return TryGetLayer(layerName, out var l) ? mask.Add(l) : mask;
    }

    /// <summary>
    /// Removes a specified layer from the LayerMask if it exists.
    /// </summary>
    /// <param name="mask">The LayerMask from which the layer will be removed.</param>
    /// <param name="layer">The layer index to be removed from the LayerMask.</param>
    /// <returns>The updated LayerMask after the specified layer has been removed.</returns>
    public static LayerMask Remove(this LayerMask mask, int layer)
    {
        return layer < MIN_LAYER || layer > MAX_LAYER ? mask : (LayerMask)(mask & ~(1 << layer));
    }

    /// <summary>
    /// Removes a specified layer from a LayerMask by its name.
    /// If the layer name does not correspond to a valid layer, the original LayerMask is returned unchanged.
    /// </summary>
    /// <param name="mask">The LayerMask from which the specified layer should be removed.</param>
    /// <param name="layerName">The name of the layer to be removed from the mask.</param>
    /// <returns>A new LayerMask with the specified layer removed, or the original LayerMask if the layer is invalid or not present.</returns>
    public static LayerMask Remove(this LayerMask mask, string layerName)
    {
        return TryGetLayer(layerName, out var l) ? mask.Remove(l) : mask;
    }

    /// <summary>
    /// Inverts a LayerMask, resulting in a new LayerMask with all previously included layers excluded
    /// and all previously excluded layers included.
    /// </summary>
    /// <param name="mask">The LayerMask to invert.</param>
    /// <returns>A new LayerMask that represents the inverted state of the input LayerMask.</returns>
    public static LayerMask Invert(this LayerMask mask) => ~mask;

    /// <summary>
    /// Determines whether the specified LayerMask is empty (has no layers set).
    /// </summary>
    /// <param name="mask">The LayerMask to check.</param>
    /// <returns>True if the LayerMask contains no layers; otherwise, false.</returns>
    public static bool IsEmpty(this LayerMask mask) => mask.value == 0;

    /// <summary>
    /// Determines whether the LayerMask represents a single layer.
    /// </summary>
    /// <param name="mask">The LayerMask to evaluate.</param>
    /// <returns>True if the LayerMask corresponds to exactly one layer; otherwise, false.</returns>
    public static bool IsSingle(this LayerMask mask)
    {
        var v = mask.value;
        return v != 0 && (v & (v - 1)) == 0;
    }

    /// <summary>
    /// Combines multiple LayerMasks into a single LayerMask.
    /// </summary>
    /// <param name="masks">An array of LayerMasks to combine.</param>
    /// <returns>A LayerMask that represents the union of the provided LayerMasks.</returns>
    public static LayerMask Combine(params LayerMask[] masks)
    {
        var v = 0;
        for (var i = 0; i < masks.Length; i++) v |= masks[i].value;
        return v;
    }

    /// <summary>
    /// Computes the intersection of two LayerMasks, resulting in a LayerMask that includes only the layers present in both inputs.
    /// </summary>
    /// <param name="a">The first LayerMask to intersect.</param>
    /// <param name="b">The second LayerMask to intersect.</param>
    /// <returns>A LayerMask representing the layers common to both input LayerMasks.</returns>
    public static LayerMask Intersect(this LayerMask a, LayerMask b) => a.value & b.value;

    /// <summary>
    /// Excludes the layers in the second LayerMask from the first LayerMask.
    /// </summary>
    /// <param name="a">The LayerMask from which layers are to be excluded.</param>
    /// <param name="b">The LayerMask containing the layers to exclude from the first LayerMask.</param>
    /// <returns>A new LayerMask with the layers in the second LayerMask excluded from the first LayerMask.</returns>
    public static LayerMask Exclude(this LayerMask a, LayerMask b) => a.value & ~b.value;

    /// <summary>
    /// Retrieves the indices of all layers included in the specified LayerMask.
    /// </summary>
    /// <param name="mask">The LayerMask to extract layer indices from.</param>
    /// <returns>An enumerable collection of layer indices included in the LayerMask.</returns>
    public static IEnumerable<int> ToLayerIndices(this LayerMask mask)
    {
        var v = mask.value;
        for (var i = MIN_LAYER; i <= MAX_LAYER; i++)
            if ((v & (1 << i)) != 0) yield return i;
    }

    /// <summary>
    /// Converts the layers represented in a LayerMask into their corresponding layer names.
    /// </summary>
    /// <param name="mask">The LayerMask containing the layers to convert to names.</param>
    /// <returns>An IEnumerable of strings representing the names of the layers included in the LayerMask.</returns>
    public static IEnumerable<string> ToLayerNames(this LayerMask mask)
    {
        foreach (var i in mask.ToLayerIndices())
            yield return LayerMask.LayerToName(i);
    }

    /// <summary>
    /// Creates a LayerMask from the specified array of layer indices.
    /// Only valid layer indices within the range of Unity's defined layers are included.
    /// </summary>
    /// <param name="layers">An array of layer indices to include in the resulting LayerMask.</param>
    /// <returns>A LayerMask containing the specified layers.</returns>
    public static LayerMask FromLayers(params int[] layers)
    {
        var v = 0;
        for (var i = 0; i < layers.Length; i++)
        {
            var l = layers[i];
            if (l >= MIN_LAYER && l <= MAX_LAYER) v |= 1 << l;
        }
        return v;
    }

    /// <summary>
    /// Creates a LayerMask from the provided layer names.
    /// </summary>
    /// <param name="layerNames">An array of layer names to include in the LayerMask.</param>
    /// <returns>A LayerMask representing the specified layers.</returns>
    public static LayerMask FromLayerNames(params string[] layerNames)
    {
        int v = 0;
        for (int i = 0; i < layerNames.Length; i++)
            if (TryGetLayer(layerNames[i], out var l)) v |= 1 << l;
        return v;
    }

    /// <summary>
    /// Converts an integer layer index to a LayerMask.
    /// </summary>
    /// <param name="layer">The layer index to convert. Must be between the minimum and maximum allowed layer indices.</param>
    /// <returns>A LayerMask containing the specified layer index, or an empty LayerMask if the layer index is invalid.</returns>
    public static LayerMask ToMask(this int layer) => (LayerMask)(layer < MIN_LAYER || layer > MAX_LAYER ? 0 : (1 << layer));

    /// <summary>
    /// Converts a collection of layer indices into a LayerMask.
    /// </summary>
    /// <param name="layers">A read-only list of layer indices to combine into a LayerMask.</param>
    /// <returns>A LayerMask representing the specified layer indices.</returns>
    public static LayerMask ToMask(this IReadOnlyList<int> layers) => FromLayers(layers is int[] a ? a : new List<int>(layers).ToArray());

    /// <summary>
    /// Determines whether the specified layer index is valid within the allowed range.
    /// </summary>
    /// <param name="layer">The layer index to validate.</param>
    /// <returns>True if the layer index is within the valid range; otherwise, false.</returns>
    public static bool IsValidLayer(int layer) => layer >= MIN_LAYER && layer <= MAX_LAYER;

    /// <summary>
    /// Attempts to retrieve the integer value of a layer by its name.
    /// </summary>
    /// <param name="layerName">The name of the layer to resolve.</param>
    /// <param name="layer">The integer value of the resolved layer if successful, or -1 if the layer does not exist.</param>
    /// <returns>True if the layer was successfully resolved; otherwise, false.</returns>
    public static bool TryGetLayer(string layerName, out int layer)
    {
        layer = LayerMask.NameToLayer(layerName);
        return layer != -1;
    }

    #endregion

    #region Layer with GameObject

    /// <summary>
    /// Determines whether the specified GameObject is included within a given LayerMask.
    /// </summary>
    /// <param name="gameObject">The GameObject to check.</param>
    /// <param name="mask">The LayerMask to validate against.</param>
    /// <returns>True if the GameObject's layer is included in the LayerMask; otherwise, false.</returns>
    public static bool IsIn(this GameObject gameObject, LayerMask mask)
    {
        if (!gameObject) return false;
        return mask.Contains(gameObject.layer);
    }

    /// <summary>
    /// Determines whether a Component is part of the specified LayerMask.
    /// </summary>
    /// <param name="comp">The Component to check.</param>
    /// <param name="mask">The LayerMask to check against.</param>
    /// <returns>True if the Component is included in the LayerMask; otherwise, false.</returns>
    public static bool IsIn(this Component comp, LayerMask mask)
    {
        return comp && comp.gameObject.IsIn(mask);
    }

    /// <summary>
    /// Assigns the specified layer to the GameObject if the layer is valid.
    /// </summary>
    /// <param name="gameObject">The GameObject to which the layer will be assigned.</param>
    /// <param name="layer">The layer index to assign to the GameObject.</param>
    public static void SetLayer(this GameObject gameObject, int layer)
    {
        if (!gameObject || !IsValidLayer(layer)) return;
        gameObject.layer = layer;
    }

    /// <summary>
    /// Sets the layer of the specified GameObject to the given layer name, if valid.
    /// </summary>
    /// <param name="gameObject">The GameObject whose layer is to be updated.</param>
    /// <param name="layerName">The name of the layer to assign to the GameObject.</param>
    public static void SetLayer(this GameObject gameObject, string layerName)
    {
        if (!gameObject || !TryGetLayer(layerName, out var l)) return;
        gameObject.layer = l;
    }

    /// <summary>
    /// Sets the layer of the specified GameObject and all its child objects recursively.
    /// </summary>
    /// <param name="gameObject">The root GameObject whose layer is to be set.</param>
    /// <param name="layer">The layer index to assign to the GameObject and its children.</param>
    /// <param name="includeInactive">Specifies whether to include inactive GameObjects in the operation.</param>
    public static void SetLayerRecursively(this GameObject gameObject, int layer, bool includeInactive = true)
    {
        if (!gameObject || !IsValidLayer(layer)) return;

        void Set(GameObject target)
        {
            target.layer = layer;
            var t = target.transform;
            int count = t.childCount;
            for (int i = 0; i < count; i++)
            {
                var child = t.GetChild(i);
                if (includeInactive || child.gameObject.activeInHierarchy)
                    Set(child.gameObject);
            }
        }

        Set(gameObject);
    }

    /// <summary>
    /// Sets the layer of the specified GameObject and all its child objects recursively.
    /// </summary>
    /// <param name="gameObject">The root GameObject to set the layer for.</param>
    /// <param name="layerName">The layer name to apply to the GameObject and its children.</param>
    /// <param name="includeInactive">Whether to include inactive child objects when setting the layer.</param>
    public static void SetLayerRecursively(this GameObject gameObject, string layerName,
        bool includeInactive = true)
    {
        if (!gameObject || !TryGetLayer(layerName, out var l)) return;
        gameObject.SetLayerRecursively(l, includeInactive);
    }

    /// <summary>
    /// Recursively sets the layer of a GameObject and its child objects based on a specified condition.
    /// </summary>
    /// <param name="gameObject">The root GameObject whose layer will be set recursively.</param>
    /// <param name="layer">The layer index to assign to the GameObject and its children if the condition is met.</param>
    /// <param name="predicate">A function that determines whether the layer should be set for a specific Transform. Returns true to set the layer, false otherwise.</param>
    /// <param name="includeInactive">Specifies whether inactive child objects should also be considered during the process. Defaults to true.</param>
    public static void SetLayerRecursivelyIf(this GameObject gameObject, int layer,
        Func<Transform, bool>? predicate, bool includeInactive = true)
    {
        if (!gameObject || !IsValidLayer(layer) || predicate == null) return;

        SetIf(gameObject.transform);
        return;

        void SetIf(Transform transform)
        {
            if (predicate(transform)) transform.gameObject.layer = layer;
            var count = transform.childCount;
            for (var i = 0; i < count; i++)
            {
                var child = transform.GetChild(i);
                if (includeInactive || child.gameObject.activeInHierarchy)
                    SetIf(child);
            }
        }
    }
        
    #endregion
}