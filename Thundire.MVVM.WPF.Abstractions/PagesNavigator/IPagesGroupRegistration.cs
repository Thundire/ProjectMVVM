namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public interface IPagesGroupRegistration
    {
        void Register<TPage>(string pageCallName) where TPage : class, INavigablePage;
    }
}