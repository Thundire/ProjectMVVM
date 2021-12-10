using System;
using System.Windows.Input;

using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Commands;

namespace Thundire.MVVM.WPF.Observable.EditForm
{
    public abstract class EditFormVM : NotifyBase
    {
        protected EditFormVM(IWpfCommandsFactory commandsFactory)
        {
            CloseFormCommand = commandsFactory.CreateAsBase(() => EndWork(new() { Result = false }));
        }

        public event EventHandler<EditFormResultArgs> OnWorkDone;

        public ICommand ConfirmCommand { get; protected init; }
        public ICommand CancelCommand { get; protected init; }

        public ICommand CloseFormCommand { get; }

        protected virtual void EndWork(EditFormResultArgs result)
        {
            OnWorkDone?.Invoke(this, result);
        }
    }
}