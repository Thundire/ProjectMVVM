using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.Linq
{
    public static class LinqExtensions
    {
        public static void Execute<T>(this IEnumerable<T> self)
        {
            foreach (var _ in self) { }
        }
    }
}