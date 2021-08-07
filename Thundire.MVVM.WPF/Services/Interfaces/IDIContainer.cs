using System;

namespace Thundire.MVVM.WPF.Services.Interfaces
{
    public interface IDIContainer
    {
        bool IsRegistered(Type type);
        object Resolve(Type type);
        TService Resolve<TService>();
    }
}