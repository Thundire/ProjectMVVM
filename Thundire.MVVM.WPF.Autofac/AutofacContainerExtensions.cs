using Autofac;
using Autofac.Builder;

using System;

using Thundire.Core.DIContainer;
using Thundire.MVVM.Core.PagesNavigator;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;
using Thundire.MVVM.WPF.Abstractions.ViewService;
using Thundire.MVVM.WPF.Core.DICache.PagesNavigator;
using Thundire.MVVM.WPF.Core.DICache.ViewService;
using Thundire.MVVM.WPF.DICache.PagesNavigator;
using Thundire.MVVM.WPF.DICache.ViewService;
using Thundire.MVVM.WPF.PagesNavigator;
using Thundire.MVVM.WPF.Regions;
using Thundire.MVVM.WPF.TemplatesCache;
using Thundire.MVVM.WPF.ViewService;

namespace Thundire.MVVM.WPF.Autofac
{
    public static class AutofacContainerExtensions
    {
        private static bool _isDIContainerWrapperRegistered;

        public static void AddViewHandlerService(this ContainerBuilder builder, Action<IViewRegister> registration)
        {
            builder.RegisterDIContainer();

            var viewRegister = new ViewRegister(new AutofacContainerBuilder(builder));
            registration.Invoke(viewRegister);

            builder.RegisterInstance(viewRegister.Build());
            builder.RegisterType<ViewRegisterCache>().As<IViewRegisterCache>();
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
            builder.RegisterDIContainer();

            builder.RegisterType<Navigator>().As<INavigator>().InstancePerDependency();
            var pagesRegister = new PagesRegistration(new AutofacContainerBuilder(builder));
            registration.Invoke(pagesRegister);
            builder.RegisterInstance(pagesRegister.GetRegister());
            builder.RegisterType<PagesContainer>().As<IPagesContainer>();
        }

        internal static void SetLifeTimeMode<TImplementer>(
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

        private static void RegisterDIContainer(this ContainerBuilder builder)
        {
            if (_isDIContainerWrapperRegistered) return;
            builder.RegisterType<AutofacContainer>().As<IDIContainer>().SingleInstance();
            _isDIContainerWrapperRegistered = true;
        }
    }
}