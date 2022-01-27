using System.Windows;

using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Autofac_QA_Test.Views
{
    public partial class NumbersEditor : Window, IWindowView
    {
        public NumbersEditor() => InitializeComponent();
    }
}
