using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.PagesNavigator
{
    public class PagesInfoRegister : IPagesInfoRegister
    {
        public PagesInfoRegister(List<PageRegistrationInfo> pages) => Register = pages;

        private List<PageRegistrationInfo> Register { get; }

        public IReadOnlyList<PageRegistrationInfo> GetGroup(string group) => Register.Where(info => info.GroupName == @group).ToImmutableList();

        public PageRegistrationInfo? GetPage(string pageName) => Register.FirstOrDefault(info => info.PageName == pageName);
    }
}