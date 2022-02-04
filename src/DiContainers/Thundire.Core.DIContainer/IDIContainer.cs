using System;

namespace Thundire.Core.DIContainer
{
    public interface IDIContainer
    {
        object Resolve(Type type);
        TService Resolve<TService>() where TService: notnull;
    }
}