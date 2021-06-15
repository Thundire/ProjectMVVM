using Autofac;
using AutofacSample.SubCode;

using System.Windows;

namespace AutofacSample
{
    public partial class App : Application
    {
        public static ILifetimeScope Services { get; } = ContainerConfiguration.CreateSimpleContainer().Build();
    }
}
