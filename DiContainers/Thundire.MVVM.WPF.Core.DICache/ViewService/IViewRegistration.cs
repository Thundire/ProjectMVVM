using System;
using Thundire.Core.DIContainer;

namespace Thundire.MVVM.WPF.Core.DICache.ViewService
{
    public interface IViewRegistration
    {
        Type View { get; }
        Type? ViewModel { get; }
        LifeTimeMode ViewMode { get; }
        LifeTimeMode ViewModelMode { get; }
        bool HasViewModel { get; }

        ViewRegistrationDescriptor Build();
    }
}