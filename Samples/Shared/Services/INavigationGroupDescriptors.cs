using System.Collections.Generic;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Shared.Services
{
    public interface INavigationGroupDescriptors
    {
        public IReadOnlyCollection<NavigationDescriptor> Pages { get; }
    }
}