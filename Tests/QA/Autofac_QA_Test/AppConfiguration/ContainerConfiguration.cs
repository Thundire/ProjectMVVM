using Autofac;
using Autofac_QA_Test.ViewModels;
using Thundire.MVVM.WPF.Autofac;

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
            return builder;
        }

        private static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            builder.AddViewHandlerService(register =>
            {
                register.Register<MainWindow, MainVM>(ViewsKeys.Main);
            });

            return builder;
        }
    }
}
