using System;

namespace Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces
{
    public interface ITemplatesRegister
    {
        void AddTemplates(Action<ITemplatesCacheBuilder> configuration);
    }
}