using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Observable.EditForm
{
    public abstract class EditFormVMBase : NotifyBase
    {
        public IWpfCommand? ConfirmCommand { get; protected init; }
        public IWpfCommand? CancelCommand { get; protected init; }

        public IWpfCommand? CloseFormCommand { get; protected init; }
    }
}