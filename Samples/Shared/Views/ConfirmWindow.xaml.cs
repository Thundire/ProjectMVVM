using System.Windows;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Shared.Views
{
    public partial class ConfirmWindow : Window, IWindowView
    {
        public ConfirmWindow() => InitializeComponent();
    }
}
