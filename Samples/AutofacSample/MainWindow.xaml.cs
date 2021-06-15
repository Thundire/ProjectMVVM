using System.Windows;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;

namespace AutofacSample
{
    public partial class MainWindow : Window, IWindowView
    {
        public MainWindow() => InitializeComponent();
    }
}
