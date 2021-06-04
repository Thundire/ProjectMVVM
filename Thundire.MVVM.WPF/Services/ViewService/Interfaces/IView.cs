using System.Windows;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IView
    {
        object DataContext { get; set; }
        void Close();
        event RoutedEventHandler Loaded;
    }
}