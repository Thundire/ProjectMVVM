using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Thundire.MVVM.Core
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T>? param)
            => param is not null ? new(param) : new();

        public static async void SafeFireAndForget(this Task task, Action<Exception>? onException = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex) when (onException is not null)
            {
                onException.Invoke(ex);
            }
        }
    }
}