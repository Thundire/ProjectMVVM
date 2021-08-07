using Autofac;
using Autofac_QA_Test.ViewModels;
using System;
using Autofac_QA_Test.RegionsTests;
using Autofac_QA_Test.RegionsTests.SinglePageRegionTest;
using Autofac_QA_Test.RegionsTests.StackViewsRegionTest;
using Thundire.MVVM.WPF.Autofac;
using Thundire.MVVM.WPF.Services.Regions;

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
            return builder;
        }

        private static ContainerBuilder RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterType<FooVM>();
            builder.RegisterType<BarVM>();
            builder.RegisterType<SinglePageRegionTestMainVM>().InstancePerDependency();
            builder.RegisterType<StackViewsRegionTestMainVM>().InstancePerDependency();
            builder.RegisterType<RegionsMainVM>();
            return builder;
        }

        private static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            builder.AddViewHandlerService(register =>
            {
                register.Register<MainWindow, MainVM>(ViewsKeys.Main);
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
