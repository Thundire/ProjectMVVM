using System;

namespace Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces
{
    public interface ITemplatesCacheBuilder
    {
        void FromResourceDictionary(Uri resourceDictionary);

        ITemplatesCacheBuilder Register(Type element, string presenterKey, Type dataType);
    }
}