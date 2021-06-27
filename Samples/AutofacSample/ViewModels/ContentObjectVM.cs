using System.Windows.Input;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;

namespace AutofacSample.ViewModels
{
    public class ContentObjectVM : NotifyBase
    {
        private readonly ChangeDescriptionVM _changeDescription;
        private string _header;
        private string _description;
        
        public ContentObjectVM(ChangeDescriptionVM changeDescription, RegionsService regionsService)
        {
            _changeDescription = changeDescription;

            EditRegion = RegionsService.GetRegion("2");
            OpenChangeDescriptionCommand = new RelayCommand(Execute);

            _changeDescription.OnConfirm = s =>
            {
                Description = s;
                EditRegion.Close();
            };
        }

        public string Header { get => _header; set => Set(ref _header, value); }
        public string Description { get => _description; set => Set(ref _description, value); }

        public IRegion EditRegion { get; set; }

        public ICommand OpenChangeDescriptionCommand { get; }

        private void Execute()
        {
            _changeDescription.Description = _description;
            EditRegion.Change(_changeDescription);
            EditRegion.Open();
        }
    }
}