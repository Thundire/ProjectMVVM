using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public interface IPagesInfoRegister
    {
        IReadOnlyList<PageRegistrationInfo> GetGroup(string group);
        PageRegistrationInfo GetPage(string pageName);
    }
}