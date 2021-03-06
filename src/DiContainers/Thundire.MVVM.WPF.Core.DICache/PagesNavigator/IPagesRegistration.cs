using System;

namespace Thundire.MVVM.WPF.Core.DICache.PagesNavigator
{
    public interface IPagesRegistration
    {
        void AddGroup(string groupName, Action<IPagesGroupRegistration> registration);
        void AddPage<TPage>(string pageName);
    }
}