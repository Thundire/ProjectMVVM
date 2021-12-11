using Autofac;

using System.Windows;
using Autofac_QA_Test.AppConfiguration;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Autofac_QA_Test
{
    public partial class App : Application
    {
        public static ILifetimeScope Services { get; } = ContainerConfiguration.CreateSimpleContainer().Build();

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
                .Show();
        }
    }
}
