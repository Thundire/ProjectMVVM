using System;

namespace Thundire.MVVM.WPF.Observable.EditBase
{
    public class ConfirmHandler<TValue>
    {
        public ConfirmHandler(Action<TValue> onConfirm, Func<TValue, bool> validation)
        {
            OnConfirm = onConfirm;
            Validation = validation;
        }

        public Action<TValue> OnConfirm { get; init; }
        public Func<TValue, bool> Validation { get; init; }
    }
}