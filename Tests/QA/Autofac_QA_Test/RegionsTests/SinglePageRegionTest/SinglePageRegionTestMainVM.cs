using Autofac_QA_Test.ViewModels;
using System.Windows.Input;

using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;
using Thundire.MVVM.WPF.Services.Regions;

namespace Autofac_QA_Test.RegionsTests.SinglePageRegionTest
{
    public class SinglePageRegionTestMainVM : NotifyBase
    {
        private IRegion Region { get; }

        public SinglePageRegionTestMainVM(IRegionsFactory regionsService)
        {
            Region = regionsService.GetRegion(RegionsKeys.SinglePageRegion);
            
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