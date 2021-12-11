using System.Windows;
using Thundire.MVVM.WPF.Abstractions.ViewHandler;

namespace Autofac_QA_Test
{
    public partial class MainWindow : Window, IWindowView
    {
        public MainWindow() => InitializeComponent();
    }
}
