using System;

namespace Thundire.MVVM.WPF.Abstractions.DependencyInjection
{
    public interface IDIContainer
    {
        object Resolve(Type type);
        TService Resolve<TService>();
    }
}