using System;
using Autofac;
using Autofac.Builder;
using Thundire.MVVM.WPF.Abstractions.DependencyInjection;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;
using Thundire.MVVM.WPF.Services;
using Thundire.MVVM.WPF.Services.Navigator;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.ViewService;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Autofac
{
    public static class AutofacContainerExtensions
    {
        public static void AddViewHandlerService(this ContainerBuilder builder, Action<IViewRegister> registration) 
        {
            var viewRegister = new ViewRegister(new AutofacContainerBuilder(builder));
            registration?.Invoke(viewRegister);
            viewRegister.Build();

            builder.RegisterType<AutofacContainer>().As<IDIContainer>().SingleInstance();
            builder.RegisterInstance(viewRegister).As<IViewRegisterCache>();
            builder.RegisterType<ViewHandlerService>().As<IViewHandlerService>();
        }

        public static void AddRegionsService(
            this ContainerBuilder builder,
            Action<ITemplatesCacheBuilder> configuration,
            Action<IRegionsRegistrator> regionsPreregistration = null)
        {
            DataTemplatesRegister register = new();
            register.AddTemplates(configuration);
            var regionsService = new RegionsService(register);
            regionsPreregistration?.Invoke(regionsService);
            builder.RegisterInstance(register).As<ITemplatesCache>();
            builder.RegisterInstance(regionsService).As<IRegionsFactory>();
        }

        public static void AddPages(this ContainerBuilder builder, Action<IPagesRegistration> registration)
        {
            builder.RegisterType<Navigator>().As<INavigator>().InstancePerDependency();
            var pagesRegister = new PagesRegistration(new AutofacContainerBuilder(builder));
            registration.Invoke(pagesRegister);
            builder.RegisterInstance(pagesRegister.GetRegister());
            builder.RegisterType<PagesContainer>().As<IPagesContainer>();
        }

        public static void SetLifeTimeMode<TImplementer>(
            this IRegistrationBuilder<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle> typeBuilder,
            LifeTimeMode mode)
        {
            switch (mode)
            {
                case LifeTimeMode.Singleton:
                    typeBuilder.SingleInstance();
                    break;
                case LifeTimeMode.Transient:
                    typeBuilder.InstancePerDependency();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}