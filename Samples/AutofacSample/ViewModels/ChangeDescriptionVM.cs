using System;
using System.Windows.Input;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;

namespace AutofacSample.ViewModels
{
    public class ChangeDescriptionVM : NotifyBase
    {
        private string _description;

        public ChangeDescriptionVM()
        {
            Confirm = new RelayCommand(() => OnConfirm(_description));
        }

        public Action<string> OnConfirm { get; set; }

        public string Description { get => _description; set => Set(ref _description, value); }

        public ICommand Confirm { get; }
    }
}