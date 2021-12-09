using Autofac_QA_Test.ViewModels;

using System.Windows.Input;

using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;

namespace Autofac_QA_Test.RegionsTests.StackViewsRegionTest
{
    public class StackViewsRegionTestMainVM : NotifyBase
    {
        private IRegion Region { get; }

        public StackViewsRegionTestMainVM(IRegionsFactory regionsService)
        {
            Region = regionsService.GetRegion(RegionsKeys.StackViewsRegion);

            OpenBarCommand = new RelayCommand(OpenBar);
            OpenFooCommand = new RelayCommand(OpenFoo);
        }

        private void OpenFoo()
        {
            Region.Change(new FooVM());
            Region.Open();
        }

        private void OpenBar()
        {
            Region.Change(new BarVM());
            Region.Open();
        }

        public ICommand OpenBarCommand { get; }
        public ICommand OpenFooCommand { get; }
    }
}