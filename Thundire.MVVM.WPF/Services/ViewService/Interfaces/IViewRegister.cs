using System;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IViewRegister
    {
        void Build();
        ViewRegister.ViewRegistrationBuilder Register<TView>(string mark = null) where TView : IView;
        ViewRegister.ViewRegistrationBuilder Register<TView, TViewModel>(string mark = null) where TView : IView;
        void AddSubViewTemplates(Action<ITemplatesRegister> templatesRegistration);
    }
}