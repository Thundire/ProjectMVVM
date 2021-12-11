using System.Windows;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IView
    {
        object DataContext { get; set; }
        void Close();
        event RoutedEventHandler Loaded;
    }
}