using System.Collections.ObjectModel;
using System.Windows.Input;
using AutofacSample.SubCode;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;

namespace AutofacSample.ViewModels
{
    public class MainVM : NotifyBase
    {
        private readonly DataFakeService _fakeService;
        private ContentObjectVM _selected;
        private ICommand _addContentCommand;
        

        public MainVM(DataFakeService fakeService, RegionsService regionsService)
        {
            Region1 = regionsService.CreateRegion("1");
            Region2 = regionsService.CreateRegion("2");

            _fakeService = fakeService;
            Contents = new(_fakeService.ContentObjectFaker.Generate(20));
            Selected = Contents[0];

            ShowRegionCommand = new RelayCommand(ShowRegion);
        }

        public ObservableCollection<ContentObjectVM> Contents { get; set; }
        public ContentObjectVM Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public IRegion Region1 { get; }

        public IRegion Region2 { get; }

        public ICommand AddContentCommand => _addContentCommand ??= new RelayCommand(() =>
        {
            Contents.Add(_fakeService.ContentObjectFaker.Generate(1)[0]);
        });

        public ICommand ShowRegionCommand { get; }

        private void ShowRegion()
        {
            Region1.Change(Selected, "Test1");
            Region1.Open();
        }
    }
}