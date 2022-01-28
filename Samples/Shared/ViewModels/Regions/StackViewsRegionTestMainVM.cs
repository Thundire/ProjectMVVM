using System.Windows.Input;
using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Abstractions.Regions;

namespace Shared.ViewModels.Regions
{
    public class StackViewsRegionTestMainVM : NotifyBase
    {
        private IRegion Region { get; }

        public StackViewsRegionTestMainVM(IRegionsFactory regionsService, IWpfCommandsFactory commandsFactory)
        {
            Region = regionsService.GetRegion(RegionsKeys.StackViewsRegion);

            OpenBarCommand = commandsFactory.Create(OpenBar);
            OpenFooCommand = commandsFactory.Create(OpenFoo);
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