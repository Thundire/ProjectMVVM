using System;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Services;
using Shared.ViewModels;
using Shared.ViewModels.Regions;
using Shared.ViewModels.ViewService;
using Shared.Views;
using Shared.Views.Pages;
using Thundire.MVVM.Core.Commands;
using Thundire.MVVM.MicrosoftDIContainer;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Commands;

namespace MicrosoftDISample
{
    internal static class ContainerConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection builder)
        {
            builder.AddSingleton<WpfCommandsFactory>();
            builder.AddSingleton<IWpfCommandsFactory>(provider => provider.GetRequiredService<WpfCommandsFactory>());
            builder.AddSingleton<ICommandsFactory>(provider => provider.GetRequiredService<WpfCommandsFactory>());
            builder.AddSingleton<ICommandsFactory>(provider => provider.GetRequiredService<WpfCommandsFactory>());
            builder
                .AddSingleton<INavigationGroupDescriptors>(new NavigationGroupDescriptors()
                    .AddDescriptor<FooVM>("Foo", "Foo")
                    .AddDescriptor<BarVM>("Bar", "Bar")
                    .AddDescriptor("Foo2", "Foo 2")
                    .Build()
                );
            return builder;
        }

        public static IServiceCollection RegisterViewModels(this IServiceCollection builder)
        {
            builder.AddTransient<FooVM>();
            builder.AddTransient<BarVM>();
            builder.AddSingleton<SinglePageRegionTestMainVM>();
            builder.AddSingleton<StackViewsRegionTestMainVM>();
            builder.AddSingleton<RegionsMainVM>();
            builder.AddSingleton<ViewOpenVM>();
            builder.AddTransient<ConfirmVM>();
            builder.AddTransient<CommandsVM>();

            return builder;
        }

        public static IServiceCollection RegisterViews(this IServiceCollection builder)
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
