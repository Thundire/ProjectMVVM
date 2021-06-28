using Autofac;

using System.Windows;
using Autofac_QA_Test.AppConfiguration;

namespace Autofac_QA_Test
{
    public partial class App : Application
    {
        public static ILifetimeScope Services { get; } = ContainerConfiguration.CreateSimpleContainer().Build();
    }
}
