using System.Windows;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Shared.Views
{
    public partial class MainWindow : Window, IWindowView
    {
        public MainWindow() => InitializeComponent();
    }
}
