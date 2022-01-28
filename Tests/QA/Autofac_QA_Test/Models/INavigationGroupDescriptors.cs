using System.Collections.Generic;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Autofac_QA_Test.Models
{
    public interface INavigationGroupDescriptors
    {
        public IReadOnlyCollection<NavigationDescriptor> Pages { get; }
    }
}