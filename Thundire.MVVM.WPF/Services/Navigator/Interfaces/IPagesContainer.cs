using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Services.Navigator.Interfaces
{
    public interface IPagesContainer
    {
        INavigablePage GetPage(string pageName);
        IReadOnlyDictionary<string, INavigablePage> GetPagesFromGroup(string group);
    }
}