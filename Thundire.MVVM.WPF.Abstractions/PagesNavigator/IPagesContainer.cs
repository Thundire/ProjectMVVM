using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public interface IPagesContainer
    {
        INavigablePage GetPage(string pageName);
        IDictionary<string, INavigablePage> GetPagesFromGroup(string group);
    }
}