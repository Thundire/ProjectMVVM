using Autofac;

using Shared;
using Shared.Services;
using Shared.ViewModels;
using Shared.ViewModels.Regions;
using Shared.ViewModels.ViewService;
using Shared.Views;
using Shared.Views.Pages;

using System;
using Thundire.MVVM.Core.Commands;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Autofac;
using Thundire.MVVM.WPF.Commands;

namespace AutofacDISample
{
    public static class ContainerConfiguration
    {
        public static ContainerBuilder CreateSimpleContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

            return builder;
        }

        private static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<WpfCommandsFactory>().As<IWpfCommandsFactory>().As<IExecutableCommandsFactory>().As<ICommandsFactory>();
            builder
                .RegisterInstance(new NavigationGroupDescriptors()
                    .AddDescriptor<FooVM>("Foo", "Foo")
                    .AddDescriptor<BarVM>("Bar", "Bar")
                    .AddDescriptor("Foo2", "Foo 2")
                    .Build()
                )
                .As<INavigationGroupDescriptors>().SingleInstance();
            return builder;
        }

        private static ContainerBuilder RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterType<FooVM>();
            builder.RegisterType<BarVM>();
            builder.RegisterType<SinglePageRegionTestMainVM>().InstancePerDependency();
            builder.RegisterType<StackViewsRegionTestMainVM>().InstancePerDependency();
            builder.RegisterType<RegionsMainVM>();
            builder.RegisterType<ViewOpenVM>();
            builder.RegisterType<ConfirmVM>();
            builder.RegisterType<CommandsVM>();
            return builder;
        }

        private static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            builder.AddViewHandlerService(register =>
            {
                register.Register<MainWindow, MainVM>(ViewsKeys.Main);
                register.Register<ConfirmWindow>(ViewsKeys.Confirm);
                register.Register<NumbersEditorWindow, NumbersEditFormVM>(ViewsKeys.NumbersEditor);
                register.Register<NavigationWindow, NavigationVM>(ViewsKeys.Navigation);
            });

            builder.AddRegionsService(cacheBuilder =>
            {
                cacheBuilder.FromResourceDictionary(new("Templates.xaml", UriKind.RelativeOrAbsolute));
            }, regions => regions
                    .RegisterSinglePageRegion(RegionsKeys.SinglePageRegion)
                    .RegisterStackViewsRegion(RegionsKeys.StackViewsRegion));

            builder.AddPages(registration =>
            {
                registration.AddGroup("NavigationGroup", pages =>
                {
                    pages.Register<FooPage>("Foo");
                    pages.Register<FooPage>("Foo2");
                    pages.Register<BarPage>("Bar");
                });
            });

            return builder;
        }
    }
}
