using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Shared;

using System;
using System.Windows;

using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace MicrosoftDISample
{
    public partial class App : Application
    {
        private static IHost? _hosting;
        private static IHost Hosting => _hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Hosting.StartAsync();

            base.OnStartup(e);

            var handler = Hosting.Services.GetRequiredService<IViewHandlerService>();
            handler
                .Search(ViewsKeys.Main)
                .OnClose((sender, args) =>
                {
                    Shutdown();
                    return default;
                })
                .Handle<IWindowView>()
                ?.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            using var host = Hosting;
            await host.StopAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((context, services) => services
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews());
    }
}
