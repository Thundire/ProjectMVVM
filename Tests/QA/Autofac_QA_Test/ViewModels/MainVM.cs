using System.Windows.Input;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;

namespace Autofac_QA_Test.ViewModels
{
    public class MainVM : NotifyBase
    {
        private string _selectedModel;
        private const string Foo = "Foo";
        private const string Bar = "Bar";

        public MainVM(RegionsService regionsService)
        {
            Region = regionsService.CreateRegion(Bar);

            SelectedModel = Models[0];
            OpenRegionCommand = new RelayCommand(OpenRegion);
        }

        private void OpenRegion()
        {
            if (SelectedModel == Foo)
            {
                Region.Change(new FooVM());
                Region.Open();
            }
            else
            {
                Region.Change(new BarVM());
                Region.Open();
            }
        }

        public IRegion Region { get; }

        public string[] Models { get; } = {Foo, Bar};
        public string SelectedModel { get => _selectedModel; set => Set(ref _selectedModel, value); }

        
        public ICommand OpenRegionCommand { get; }
    }
}