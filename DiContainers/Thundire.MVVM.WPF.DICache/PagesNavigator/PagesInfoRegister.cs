using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Thundire.MVVM.WPF.Core.DICache.PagesNavigator;

namespace Thundire.MVVM.WPF.DICache.PagesNavigator
{
    public class PagesInfoRegister : IPagesInfoRegister
    {
        public PagesInfoRegister(HashSet<PageRegistrationInfo> pages) => Register = pages;

        private HashSet<PageRegistrationInfo> Register { get; }

        public IReadOnlyList<PageRegistrationInfo> GetGroup(string group) => Register.Where(info => info.GroupName == @group).ToImmutableList();

        public PageRegistrationInfo? GetPage(string pageName) => Register.FirstOrDefault(info => info.PageName == pageName);
    }
}