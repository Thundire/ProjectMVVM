using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewRegisterCache
    {
        IReadOnlyList<ViewRegistration> Registrations { get; }
    }
}