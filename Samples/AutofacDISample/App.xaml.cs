using Autofac;
using Shared;
using System.Windows;

using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace AutofacDISample
{
    public partial class App : Application
    {
        private static ILifetimeScope Services { get; } = ContainerConfiguration.CreateSimpleContainer().Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var handler = Services.Resolve<IViewHandlerService>();
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
    }
}
