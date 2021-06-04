using System.Collections.Generic;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IViewRegisterCache
    {
        IReadOnlyList<ViewRegistration> Registrations { get; }
    }
}