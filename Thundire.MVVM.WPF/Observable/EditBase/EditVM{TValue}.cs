using System;
using Thundire.MVVM.WPF.Commands.Relay;

namespace Thundire.MVVM.WPF.Observable.EditBase
{
    public class EditVM<TValue> : EditVM where TValue : new()
    {
        public EditVM()
        {

        }

        public EditVM(TValue value, ConfirmHandler<TValue> confirmHandler, Action onCancel) : base(value ?? new TValue(), onCancel)
        {
            ConfirmCommand = new RelayCommand<object>(o => confirmHandler.OnConfirm((TValue)o), o => o is not null && confirmHandler.Validation((TValue)o));
        }
    }
}