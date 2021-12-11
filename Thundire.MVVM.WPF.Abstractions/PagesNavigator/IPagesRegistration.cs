using System;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public interface IPagesRegistration
    {
        void AddGroup(string groupName, Action<IPagesGroupRegistration> registration);
        void AddPage<TPage>(string pageName);
    }
}