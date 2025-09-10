
using System.Collections.Generic;

namespace UnityProjectFramework.Core;

/// <summary>
/// Provides extension methods for list manipulation, offering utilities for efficient element swapping and removal.
/// </summary>
public static class IteratorListExtensions
{
    /// <summary>
    /// Removes the element at the specified index from the list by swapping it with the last element
    /// and then removing the last element. Outputs the value swapped to the specified index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list from which the element will be removed.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <param name="swappedValue">Outputs the value swapped into the specified index, or the default value if no swap occurred.</param>
    public static void RemoveAtSwapBack<T>(this IList<T> list, int index, out T swappedValue)
    {
        var lastIndex = list.Count - 1;
        if (lastIndex > 0 && lastIndex != index)
        {
            swappedValue = list[index] = list[lastIndex];
        }
        else
        {
            swappedValue = default;
        }
            
        list.RemoveAt(lastIndex);
    }

    /// <summary>
    /// Swaps the elements at the specified indices in the list and outputs the value of the first swapped element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list on which the swap operation will be performed.</param>
    /// <param name="indexA">The index of the first element to be swapped.</param>
    /// <param name="indexB">The index of the second element to be swapped.</param>
    /// <param name="newValue">Outputs the value of the element at the first index before swapping.</param>
    public static void Swap<T>(this IList<T> list, int indexA, int indexB, out T newValue)
    {
        newValue = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = newValue;
    }
}