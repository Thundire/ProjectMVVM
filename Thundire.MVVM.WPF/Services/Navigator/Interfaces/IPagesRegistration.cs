using System;

namespace Thundire.MVVM.WPF.Services.Navigator.Interfaces
{
    public interface IPagesRegistration
    {
        void AddGroup(string groupName, Action<IPagesGroupRegistration> registration);
        void AddPage<TPage>(string pageName);
    }
}