using System;

namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public class NavigationDescriptor
    {
        public int Order { get; init; }
        public string PageKey { get; init; }
        public string Title { get; init; }
        public object? DataContext { get; set; }
        public Type? DataContextType { get; init; }
    }
}