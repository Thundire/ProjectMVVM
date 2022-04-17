using System;
using System.Collections.ObjectModel;
using System.Windows;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Abstractions
{
    public static class Extensions
    {
        public static void DispatcherAdd<T>(this ObservableCollection<T> self, T item)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate { self.Add(item); });
        }

        public static bool Set<T>(this IWpfCommand self, ref T field, T value, bool isExecutingDependsOnIt)
        {
            if (Equals(field, value)) return false;
            field = value;
            if (isExecutingDependsOnIt) self.RaiseCanExecuteChanged();
            return true;
        }
    }
}