using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.Core.DICache.ViewService
{
    public interface IViewRegister
    {
        IViewRegistrationBuilder Register<TView>(string mark) where TView : IView;
        IViewRegistrationBuilder Register<TView, TViewModel>(string mark) where TView : IView;
    }
}