using System;
using Thundire.MVVM.WPF.Abstractions.DependencyInjection;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewRegistration
    {
        Type View { get; }
        Type ViewModel { get; }
        LifeTimeMode ViewMode { get; }
        LifeTimeMode ViewModelMode { get; }

        ViewRegistration Build();
    }
}