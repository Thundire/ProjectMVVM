using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    public static class GenericCollectionsExtensions
    {
        /// <summary>
        /// Validates that the <paramref name="enumerable"/> is not null and contains items.
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? enumerable) => enumerable is not null && enumerable.Any();
    }
}