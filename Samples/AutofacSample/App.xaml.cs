using Autofac;

using System.Windows;

namespace AutofacSample
{
    public partial class App : Application
    {
        public static ILifetimeScope Services { get; } = AutofacContainer.CreateSimpleContainer().Build();
    }
}
