using Microsoft.Extensions.DependencyInjection;

using System;
using Thundire.Core.DIContainer;

namespace Thundire.MVVM.MicrosoftDIContainer
{
    public class MSContainerRegistrator : IDIContainerBuilder
    {
        private readonly IServiceCollection _services;

        public MSContainerRegistrator(IServiceCollection services)
        {
            _services = services;
        }

        public void RegisterType(Type type, LifeTimeMode mode)
        {
            if (mode == LifeTimeMode.Singleton)
            {
                _services.AddSingleton(type);
                return;
            }

            _services.AddTransient(type);
        }

        public void RegisterTypeAs(Type type, Type asType, LifeTimeMode mode)
        {
            ServiceLifetime lifeTime = mode is LifeTimeMode.Singleton
                ? ServiceLifetime.Singleton
                : ServiceLifetime.Transient;

            _services.Add(new ServiceDescriptor(asType, type, lifeTime));
        }
    }
}