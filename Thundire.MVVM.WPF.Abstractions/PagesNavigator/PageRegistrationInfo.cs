using System;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public class PageRegistrationInfo
    {
        public PageRegistrationInfo(string pageName, Type pageType) : this(pageName, null, pageType) { }

        public PageRegistrationInfo(string pageName, string? groupName, Type pageType)
        {
            PageName = pageName;
            GroupName = groupName;
            PageType = pageType;
        }

        public string PageName { get; }
        public string? GroupName { get; }
        public Type PageType { get; }
    }
}