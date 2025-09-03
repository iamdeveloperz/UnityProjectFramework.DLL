
using System.Collections.Generic;

namespace UnityProjectFramework.Core
{
    public static class IteratorListExtensions
    {
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
        
        public static void Swap<T>(this IList<T> list, int indexA, int indexB, out T newValue)
        {
            newValue = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = newValue;
        }
    }
}