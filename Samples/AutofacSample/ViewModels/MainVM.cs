using System.Collections.ObjectModel;
using System.Windows.Input;
using AutofacSample.SubCode;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;

namespace AutofacSample.ViewModels
{
    public class MainVM : NotifyBase
    {
        private readonly DataFakeService _fakeService;
        private ContentObjectVM _selected;
        private ICommand _addContentCommand;
        

        public MainVM(DataFakeService fakeService)
        {
            _fakeService = fakeService;
            Contents = new(_fakeService.ContentObjectFaker.Generate(20));
            Selected = Contents[0];
        }

        public ObservableCollection<ContentObjectVM> Contents { get; set; }
        public ContentObjectVM Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public ICommand AddContentCommand => _addContentCommand ??= new RelayCommand(() =>
        {
            Contents.Add(_fakeService.ContentObjectFaker.Generate(1)[0]);
        });
    }
}