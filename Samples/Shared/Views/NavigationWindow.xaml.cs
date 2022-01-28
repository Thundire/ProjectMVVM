using System.Windows;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Shared.Views
{
    public partial class NavigationWindow : Window, IWindowView
    {
        public NavigationWindow() => InitializeComponent();
    }
}
