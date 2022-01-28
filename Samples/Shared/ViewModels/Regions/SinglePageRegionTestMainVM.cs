using System.Diagnostics;
using System.Windows.Input;
using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Abstractions.Regions;

namespace Shared.ViewModels.Regions
{
    public class SinglePageRegionTestMainVM : NotifyBase
    {
        private IRegion Region { get; }

        public SinglePageRegionTestMainVM(IRegionsFactory regionsService, IWpfCommandsFactory commandsFactory)
        {
            Region = regionsService.GetRegion(RegionsKeys.SinglePageRegion);

            OpenBarCommand = commandsFactory.Create(OpenBar);
            OpenFooCommand = commandsFactory.Create(OpenFoo);
            CloseRegionCommand = commandsFactory.Create(() =>
            {
                Region.Close();
                Debug.WriteLine("Closed");
            });
        }

        private void OpenFoo()
        {
            Region.Change(new FooVM());
            Region.Open();
            Debug.WriteLine("Open");
        }

        private void OpenBar()
        {
            Region.Change(new BarVM());
            Region.Open();
        }

        public ICommand OpenBarCommand { get; }
        public ICommand OpenFooCommand { get; }
        public ICommand CloseRegionCommand { get; }
    }
}