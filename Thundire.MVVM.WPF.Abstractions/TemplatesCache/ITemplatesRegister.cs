using System;

namespace Thundire.MVVM.WPF.Abstractions.TemplatesCache
{
    public interface ITemplatesRegister
    {
        void AddTemplates(Action<ITemplatesCacheBuilder> configuration);
    }
}