namespace Thundire.MVVM.WPF.Core.DICache.ViewService
{
    public interface IViewRegistrationBuilder
    {
        IViewRegistration AsRegistration();
        IViewRegistrationBuilder ViewAsSingleton();
        IViewRegistrationBuilder ViewAsTransient();
        IViewRegistrationBuilder ViewModelAsSingleton();
        IViewRegistrationBuilder ViewModelAsTransient();
        IViewRegistrationBuilder WithViewModel<TViewModel>();
    }
}