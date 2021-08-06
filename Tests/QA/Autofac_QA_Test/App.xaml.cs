using Autofac;

using System.Windows;
using Autofac_QA_Test.AppConfiguration;
using Autofac_QA_Test.RegionsTests;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;

namespace Autofac_QA_Test
{
    public partial class App : Application
    {
        public static ILifetimeScope Services { get; } = ContainerConfiguration.CreateSimpleContainer().Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var regionsService = Services.Resolve<RegionsService>();
            regionsService.CreateSinglePageRegion(RegionsKeys.SinglePageRegion);
            regionsService.CreateStackViewsRegion(RegionsKeys.StackViewsRegion);

            var handler = Services.Resolve<IViewHandlerService>();
            handler
                .Search(ViewsKeys.Main)
                .OnClose((sender, args) =>
                {
                    Shutdown();
                    return default;
                })
                .Handle<IWindowView>()
                .Show();
        }
    }
}
