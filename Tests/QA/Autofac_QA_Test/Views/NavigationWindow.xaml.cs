using System.Windows;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Autofac_QA_Test.Views
{
    public partial class NavigationWindow : Window, IWindowView
    {
        public NavigationWindow() => InitializeComponent();
    }
}
