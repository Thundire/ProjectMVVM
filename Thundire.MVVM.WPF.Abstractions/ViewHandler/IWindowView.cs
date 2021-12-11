using System;
using System.Windows;
using System.Windows.Threading;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IWindowView : IView
    {
        event EventHandler Closed;
        Dispatcher Dispatcher { get; }
        Window Owner { get; set; }
        WindowStartupLocation WindowStartupLocation { get; set; }

        void Show();
        bool? ShowDialog();

        public void SetOwner(object parent)
        {
            var window = parent as Window;
            Owner = window;
        }
    }
}