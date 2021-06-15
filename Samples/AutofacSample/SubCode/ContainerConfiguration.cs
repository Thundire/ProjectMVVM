﻿using Autofac;
using AutofacSample.ViewModels;

using System;

using Thundire.MVVM.WPF.Autofac;

namespace AutofacSample.SubCode
{
    public static class ContainerConfiguration
    {
        // use this method if you need to inject autofac in Microsoft Dependency injection 
        // usage:
        // public static IHostBuilder CreateHostBuilder(string[] args) => Host
        //       .CreateDefaultBuilder(args)
        //       .UseServiceProviderFactory(ContainerConfiguration.GetFactory)
        //
        // public static IServiceProviderFactory<ContainerBuilder> GetFactory(HostBuilderContext context)
        // {
        //     return new AutofacServiceProviderFactory(builder => builder.ConfigureContainerServices());
        // }

        public static ContainerBuilder CreateSimpleContainer()
        {
            var builder = new ContainerBuilder();

            builder.ConfigureContainerServices();

            return builder;
        }

        private static void ConfigureContainerServices(this ContainerBuilder builder)
            => builder
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews()
        ;

        private static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<DataFakeService>();
            return builder;
        }

        private static ContainerBuilder RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterType<ContentObjectVM>().InstancePerDependency();
            builder.RegisterType<ChangeDescriptionVM>().InstancePerDependency();
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
                cacheBuilder.FromResourceDictionary(new("Resources/DataTemplates.xaml", UriKind.RelativeOrAbsolute));
            });

            return builder;
        }
    }
}
