using Autofac;

using Autofac_QA_Test.RegionsTests;
using Autofac_QA_Test.RegionsTests.SinglePageRegionTest;
using Autofac_QA_Test.RegionsTests.StackViewsRegionTest;
using Autofac_QA_Test.ViewModels;

using System;
using Autofac_QA_Test.Views;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Autofac;
using Thundire.MVVM.WPF.Commands;

namespace Autofac_QA_Test.AppConfiguration
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
            builder.RegisterType<WpfCommandsFactory>().As<IWpfCommandsFactory>();
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
            return builder;
        }

        private static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            builder.AddViewHandlerService(register =>
            {
                register.Register<MainWindow, MainVM>(ViewsKeys.Main);
                register.Register<Confirm>(ViewsKeys.Confirm);
                register.Register<NumbersEditor, NumbersEditFormVM>(ViewsKeys.NumbersEditor);
            });

            builder.AddRegionsService(cacheBuilder =>
            {
                cacheBuilder.FromResourceDictionary(new("Templates.xaml", UriKind.RelativeOrAbsolute));
            }, regions => regions
                    .RegisterSinglePageRegion(RegionsKeys.SinglePageRegion)
                    .RegisterStackViewsRegion(RegionsKeys.StackViewsRegion));

            return builder;
        }
    }
}
