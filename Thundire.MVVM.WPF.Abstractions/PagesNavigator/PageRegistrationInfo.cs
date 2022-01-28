using System;
using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public class PageRegistrationInfo : IEquatable<PageRegistrationInfo?>
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

        public override bool Equals(object? obj) => Equals(obj as PageRegistrationInfo);

        public bool Equals(PageRegistrationInfo? other) =>
            other != null &&
            PageName == other.PageName &&
            GroupName == other.GroupName &&
            EqualityComparer<Type>.Default.Equals(PageType, other.PageType);

        public override int GetHashCode() => HashCode.Combine(PageName, GroupName, PageType);

        public static bool operator ==(PageRegistrationInfo? left, PageRegistrationInfo? right) => EqualityComparer<PageRegistrationInfo>.Default.Equals(left, right);

        public static bool operator !=(PageRegistrationInfo? left, PageRegistrationInfo? right) => !(left == right);
    }
}