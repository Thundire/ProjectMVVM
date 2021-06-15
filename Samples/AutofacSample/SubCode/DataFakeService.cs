using Autofac;

using AutofacSample.ViewModels;

using Bogus;

namespace AutofacSample.SubCode
{
    public class DataFakeService
    {
        private static ILifetimeScope _containerScope;

        public DataFakeService(ILifetimeScope containerScope)
        {
            _containerScope = containerScope;
        }

        public Faker<ContentObjectVM> ContentObjectFaker { get; } = new Faker<ContentObjectVM>()
            .CustomInstantiator(f => _containerScope.Resolve<ContentObjectVM>())
            .Rules((f, o) =>
            {
                o.Header = f.Name.JobType();
                o.Description = f.Lorem.Text();
            });
    }
}