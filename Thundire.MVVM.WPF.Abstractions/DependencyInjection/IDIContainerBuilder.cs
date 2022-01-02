using System;

namespace Thundire.MVVM.WPF.Abstractions.DependencyInjection
{
    public interface IDIContainerBuilder
    {
        void RegisterType(Type type, LifeTimeMode mode);
        void RegisterTypeAs(Type type, Type asType, LifeTimeMode mode);
    }
}