using System.Threading.Tasks;
using Autofac;
using AutofacSample.SubCode;

using System.Windows;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;

namespace AutofacSample
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
                    return ValueTask.CompletedTask;
                })
                .Handle<IWindowView>()
                .Show();
        }
    }
}
