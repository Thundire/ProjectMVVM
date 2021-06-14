namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IViewRegistrationBuilder
    {
        IViewRegistration AsRegistration();
        IViewRegistrationBuilder MarkAs(string mark);
        IViewRegistrationBuilder ViewAsSingleton();
        IViewRegistrationBuilder ViewAsTransient();
        IViewRegistrationBuilder ViewModelAsSingleton();
        IViewRegistrationBuilder ViewModelAsTransient();
        IViewRegistrationBuilder WithViewModel<TViewModel>();
    }
}