using Thundire.MVVM.Core.PagesNavigator;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.Core.DICache.PagesNavigator
{
    public interface IPagesGroupRegistration
    {
        void Register<TPage>(string pageCallName) where TPage : class, INavigablePage;
    }
}