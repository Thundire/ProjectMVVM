using System;

namespace Thundire.MVVM.WPF.Abstractions.TemplatesCache
{
    public interface ITemplatesCacheBuilder
    {
        void FromResourceDictionary(Uri resourceDictionary);

        ITemplatesCacheBuilder Register(Type element, string presenterKey, Type dataType);
    }
}