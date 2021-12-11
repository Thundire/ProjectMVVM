namespace Thundire.MVVM.WPF.Abstractions.ViewService
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