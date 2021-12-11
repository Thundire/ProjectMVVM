using System;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public class PageRegistrationInfo
    {
        public string PageName { get; init; }
        public string GroupName { get; init; }
        public Type PageType { get; init; }
    }
}