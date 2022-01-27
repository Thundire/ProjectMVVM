using System;
using Microsoft.Extensions.DependencyInjection;
using Thundire.MVVM.WPF.Abstractions.DependencyInjection;

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
