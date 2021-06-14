using System;

namespace Thundire.MVVM.WPF.Services.Interfaces
{
    public interface ITemplatesRegister
    {
        void AddTemplates(Action<ITemplatesCacheBuilder> configuration);
    }
}