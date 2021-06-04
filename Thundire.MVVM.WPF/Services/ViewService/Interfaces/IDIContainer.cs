using System;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IDIContainer
    {
        bool IsRegistered(Type type);
        object Resolve(Type type);
    }
}