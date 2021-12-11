using System.Windows.Navigation;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public interface INavigationHandler
    {
        NavigationService NavigationService { get; set; }
    }
}