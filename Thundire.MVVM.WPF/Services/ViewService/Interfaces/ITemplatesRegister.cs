using System;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface ITemplatesRegister
    {
        TemplatesRegister AddGroup(string group, Action<ITemplatesCacheBuilder> configuration);
    }
}