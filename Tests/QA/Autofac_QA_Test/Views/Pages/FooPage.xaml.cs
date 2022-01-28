using System.Windows.Controls;

using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Autofac_QA_Test.Views.Pages
{
    public partial class FooPage : Page, INavigablePage
    {
        public FooPage() => InitializeComponent();
    }
}
