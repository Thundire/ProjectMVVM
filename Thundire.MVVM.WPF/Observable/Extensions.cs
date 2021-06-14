using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Thundire.MVVM.WPF.Observable
{
    public static class Extensions
    {
        public static void DispatcherAdd<T>(this ObservableCollection<T> self, T item)
        {
            Application.Current.Dispatcher.BeginInvoke( (Action)delegate { self.Add(item); });
        }

        public static void AddRange<T>(this ObservableCollection<T> self, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                self.Add(item);
            }
        }
    }
}