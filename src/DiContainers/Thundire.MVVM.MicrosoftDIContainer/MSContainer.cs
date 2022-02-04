using System;
using Microsoft.Extensions.DependencyInjection;
using Thundire.Core.DIContainer;

namespace Thundire.MVVM.MicrosoftDIContainer
{
    public class MSContainer : IDIContainer
    {
        private readonly IServiceProvider _provider;

        public MSContainer(IServiceProvider provider)
        {
            _provider = provider;
        }

        public object Resolve(Type type)
        {
            if (_provider.GetService(type) is not { } service)
            {
                throw new InvalidOperationException($"Service to type: {type.FullName} not registered");
            }

            return service;
        }

        public TService Resolve<TService>() where TService: notnull => _provider.GetRequiredService<TService>();
    }
}
