using Thundire.MVVM.WPF.Observable.Base;

namespace AutofacSample.ViewModels
{
    public class ContentObjectVM : NotifyBase
    {
        private readonly ChangeDescriptionVM _changeDescription;
        private string _header;
        private string _description;
        
        public ContentObjectVM(ChangeDescriptionVM changeDescription)
        {
            _changeDescription = changeDescription;
        }

        public string Header { get => _header; set => Set(ref _header, value); }
        public string Description { get => _description; set => Set(ref _description, value); }
    }
}