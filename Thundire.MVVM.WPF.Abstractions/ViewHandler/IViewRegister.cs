namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IViewRegister
    {
        void Build();
        IViewRegistrationBuilder Register<TView>(string mark = null) where TView : IView;
        IViewRegistrationBuilder Register<TView, TViewModel>(string mark = null) where TView : IView;
    }
}