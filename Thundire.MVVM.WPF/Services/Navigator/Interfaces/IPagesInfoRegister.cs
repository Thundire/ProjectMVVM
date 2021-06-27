using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Services.Navigator.Interfaces
{
    public interface IPagesInfoRegister
    {
        IReadOnlyList<PageRegistrationInfo> GetGroup(string group);
        PageRegistrationInfo GetPage(string pageName);
    }
}