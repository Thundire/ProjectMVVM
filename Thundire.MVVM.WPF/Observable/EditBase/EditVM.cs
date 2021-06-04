using System;
using System.Windows.Input;
using Thundire.MVVM.WPF.Commands.Relay;
using Thundire.MVVM.WPF.Observable.Base;

namespace Thundire.MVVM.WPF.Observable.EditBase
{
    public class EditVM : NotifyBase
    {
        private ICommand _confirmCommand;
        private ICommand _cancelCommand;

        private object _value;

        public EditVM()
        {

        }
        protected EditVM(object value, Action onCancel)
        {
            Value = value;
            CancelCommand = new RelayCommand(onCancel);
        }

        public EditVM(object value, ConfirmHandler<object> confirmHandler, Action onCancel) : this(value, onCancel)
        {
            ConfirmCommand = new RelayCommand<object>(confirmHandler.OnConfirm, confirmHandler.Validation);
        }

        public object Value { get => _value; set => Set(ref _value, value); }

        public ICommand ConfirmCommand { get => _confirmCommand; set => Set(ref _confirmCommand, value); }
        public ICommand CancelCommand { get => _cancelCommand; set => Set(ref _cancelCommand, value); }
    }
}