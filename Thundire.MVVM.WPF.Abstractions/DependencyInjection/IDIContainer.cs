using System;

namespace Thundire.MVVM.WPF.Abstractions.DependencyInjection
{
    public interface IDIContainer
    {
        bool IsRegistered(Type type);
        object Resolve(Type type);
        TService Resolve<TService>();
    }
}