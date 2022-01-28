using System.Windows.Controls;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Shared.Views.Pages
{
    public partial class FooPage : Page, INavigablePage
    {
        public FooPage() => InitializeComponent();
    }
}
