using System.Collections.Generic;
using System.Collections.Immutable;
using Shared.Models;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Shared.Services
{
    public class NavigationGroupDescriptors : INavigationGroupDescriptors
    {
        public NavigationGroupDescriptors() => PagesMutable = new List<NavigationDescriptor>();

        public IReadOnlyCollection<NavigationDescriptor> Pages { get; private set; }
        private ICollection<NavigationDescriptor>? PagesMutable { get; set; }

        public NavigationGroupDescriptors AddDescriptor<TDataContext>(string pageKey, string alias)
        {
            PagesMutable?.Add(new NavigationDescriptor(pageKey, alias)
            {
                Order = PagesMutable.Count,
                DataContextType = typeof(TDataContext)
            });
            return this;
        }

        public NavigationGroupDescriptors AddDescriptor(string pageKey, string alias)
        {
            PagesMutable?.Add(new NavigationDescriptor(pageKey, alias)
            {
                Order = PagesMutable.Count,
            });
            return this;
        }

        public INavigationGroupDescriptors Build()
        {
            Pages = PagesMutable?.ToImmutableArray() ?? ImmutableArray<NavigationDescriptor>.Empty;
            PagesMutable = null;
            return this;
        }
    }
}