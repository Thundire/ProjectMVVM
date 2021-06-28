using Autofac;

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
            return builder;
        }

        private static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            return builder;
        }
    }
}
