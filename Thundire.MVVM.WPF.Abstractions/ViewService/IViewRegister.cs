namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewRegister
    {
        void Build();
        IViewRegistrationBuilder Register<TView>(string mark) where TView : IView;
        IViewRegistrationBuilder Register<TView, TViewModel>(string mark) where TView : IView;
    }
}