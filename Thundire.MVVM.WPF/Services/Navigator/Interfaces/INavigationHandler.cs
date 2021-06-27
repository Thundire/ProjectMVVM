using System.Windows.Navigation;

namespace Thundire.MVVM.WPF.Services.Navigator.Interfaces
{
    public interface INavigationHandler
    {
        NavigationService NavigationService { get; set; }
    }
}