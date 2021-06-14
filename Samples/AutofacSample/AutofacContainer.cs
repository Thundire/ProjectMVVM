using Autofac;

namespace AutofacSample
{
    public static class AutofacContainer
    {
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
            return builder;
        }

        private static ContainerBuilder RegisterViewModels(this ContainerBuilder builder)
        {
            return builder;
        }

        private static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            return builder;
        }
    }
}