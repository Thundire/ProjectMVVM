using System;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
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