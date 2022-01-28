
using System.Collections.Generic;
using System.Collections.Immutable;

using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Autofac_QA_Test.Models
{
    public class NavigationGroupDescriptors : INavigationGroupDescriptors
    {
        public NavigationGroupDescriptors() => PagesMutable = new List<NavigationDescriptor>();

        public IReadOnlyCollection<NavigationDescriptor> Pages { get; private set; }
        private ICollection<NavigationDescriptor>? PagesMutable { get; set; }

        public NavigationGroupDescriptors AddDescriptor<TDataContext>(string pageKey, string title)
        {
            PagesMutable?.Add(new NavigationDescriptor()
            {
                Order = PagesMutable.Count,
                PageKey = pageKey,
                Title = title,
                DataContextType = typeof(TDataContext)
            });
            return this;
        }

        public NavigationGroupDescriptors AddDescriptor(string pageKey, string title)
        {
            PagesMutable?.Add(new NavigationDescriptor()
            {
                Order = PagesMutable.Count,
                PageKey = pageKey,
                Title = title
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