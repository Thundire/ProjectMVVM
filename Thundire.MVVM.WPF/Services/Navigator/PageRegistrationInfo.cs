using System;

namespace Thundire.MVVM.WPF.Services.Navigator
{
    public class PageRegistrationInfo
    {
        public string PageName { get; init; }
        public string GroupName { get; init; }
        public Type PageType { get; init; }
    }
}