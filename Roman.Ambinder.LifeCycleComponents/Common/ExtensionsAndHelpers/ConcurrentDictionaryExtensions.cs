using System.Collections.Concurrent;

namespace Roman.Ambinder.LifeCycleComponents.Common.ExtensionsAndHelpers
{
    public static class ConcurrentDictionaryExtensions
    {
        public static void AddWithTypeBasedOnUniqueKey<T>(this ConcurrentDictionary<string, T> target,
            in T instance)
        {
            var typeName = typeof(T).FullName;
            var key = typeName;
            var prefixCounter = 1;
            // ReSharper disable once AssignNullToNotNullAttribute
            while (!target.TryAdd(key, instance))
            {
                key = $"{typeName} {++prefixCounter}";
            }
        }
    }
}