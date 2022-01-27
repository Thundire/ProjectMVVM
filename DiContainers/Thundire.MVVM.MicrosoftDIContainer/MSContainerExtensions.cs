using System;
using Microsoft.Extensions.DependencyInjection;
using Thundire.Core.DIContainer;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;
using Thundire.MVVM.WPF.Abstractions.ViewService;
using Thundire.MVVM.WPF.PagesNavigator;
using Thundire.MVVM.WPF.Regions;
using Thundire.MVVM.WPF.TemplatesCache;
using Thundire.MVVM.WPF.ViewService;

namespace Thundire.MVVM.MicrosoftDIContainer
{
    public static class MSContainerExtensions
    {
        private static bool _isDIContainerWrapperRegistered;

        public static void AddViewHandlerService(this IServiceCollection services, Action<IViewRegister> registration)
        {
            services.RegisterDIContainer();

            var viewRegister = new ViewRegister(new MSContainerRegistrator(services));
            registration?.Invoke(viewRegister);
            viewRegister.Build();

            
            services.AddSingleton<IViewRegisterCache>(viewRegister);
            services.AddSingleton<IViewHandlerService, ViewHandlerService>();
        }

        public static void AddRegionsService(
            this IServiceCollection services,
            Action<ITemplatesCacheBuilder> configuration,
            Action<IRegionsRegistrator> regionsPreregistration = null)
        {
            DataTemplatesRegister register = new();
            register.AddTemplates(configuration);

            var regionsService = new RegionsService(register);
            regionsPreregistration?.Invoke(regionsService);

            services.AddSingleton<ITemplatesCache>(register);
            services.AddSingleton<IRegionsFactory>(regionsService);
        }

        public static void AddPages(this IServiceCollection services, Action<IPagesRegistration> registration)
        {
            services.RegisterDIContainer();

            services.AddTransient<INavigator, Navigator>();

            var pagesRegister = new PagesRegistration(new MSContainerRegistrator(services));
            registration.Invoke(pagesRegister);
            services.AddSingleton(pagesRegister.GetRegister());
            services.AddSingleton<IPagesContainer, PagesContainer>();
        }

        private static void RegisterDIContainer(this IServiceCollection services)
        {
            if(_isDIContainerWrapperRegistered) return;
            services.AddSingleton<IDIContainer, MSContainer>();
            _isDIContainerWrapperRegistered = true;
        }
    }
}