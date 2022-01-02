using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Thundire.MVVM.Core
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T>? param)
            => param is not null ? new(param) : new();

        public static void AddRange<T>(this ObservableCollection<T> self, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                self.Add(item);
            }
        }
    }
}