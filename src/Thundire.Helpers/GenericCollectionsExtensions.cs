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

        /// <summary>
        /// Get hashcode from all members of enumerable and merge it
        /// </summary>
        /// <typeparam name="T">Item</typeparam>
        /// <param name="enumerable">Enumerable of Items</param>
        /// <returns>0 if enumerable is null or not contains not null values, if opposite returns merged hashcode of enumerable members</returns>
        public static int GetSequenceHashCode<T>(this IEnumerable<T>? enumerable)
        {
            if (enumerable is null) return 0;
            HashCode hash = new();

            foreach (var value in enumerable.Where(v => v is not null))
                hash.Add(value!.GetHashCode());

            return hash.ToHashCode();
        }
    }
}