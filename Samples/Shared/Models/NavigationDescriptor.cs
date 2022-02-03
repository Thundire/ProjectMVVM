using System;

namespace Shared.Models
{
    public class NavigationDescriptor
    {
        public NavigationDescriptor(string pageKey, string alias)
        {
            PageKey = pageKey;
            Alias = alias;
        }

        public int Order { get; init; }
        public string PageKey { get; }
        public string Alias { get; }
        public object? DataContext { get; set; }
        public Type? DataContextType { get; init; }
    }
}