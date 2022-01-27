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

        public object Resolve(Type type) => _provider.GetService(type);

        public TService Resolve<TService>() => _provider.GetRequiredService<TService>();
    }
}
