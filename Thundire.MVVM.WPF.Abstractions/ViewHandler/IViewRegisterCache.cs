using System.Collections.Generic;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IViewRegisterCache
    {
        IReadOnlyList<ViewRegistration> Registrations { get; }
    }
}