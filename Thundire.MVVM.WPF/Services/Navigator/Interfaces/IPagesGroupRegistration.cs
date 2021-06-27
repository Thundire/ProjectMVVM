namespace Thundire.MVVM.WPF.Services.Navigator.Interfaces
{
    public interface IPagesGroupRegistration
    {
        void Register<TPage>(string pageCallName) where TPage : class, INavigablePage;
    }
}