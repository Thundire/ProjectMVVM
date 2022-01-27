using System;

namespace Thundire.Core.DIContainer
{
    public interface IDIContainerBuilder
    {
        void RegisterType(Type type, LifeTimeMode mode);
        void RegisterTypeAs(Type type, Type asType, LifeTimeMode mode);
    }
}