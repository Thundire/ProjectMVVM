using System;
using System.ComponentModel;

using Thundire.Helpers;
using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Observable.EditForm
{
    public abstract class EditFormVM : NotifyBase
    {
        protected EditFormVM(IWpfCommandsFactory commandsFactory)
        {
            CloseFormCommand = commandsFactory.Create(() => EndWork(Result.Exit));
        }

        public event EventHandler<Result>? OnWorkDone;

        public IWpfCommand? ConfirmCommand { get; protected init; }
        public IWpfCommand? CancelCommand { get; protected init; }

        public IWpfCommand? CloseFormCommand { get; protected init; }

        protected virtual void EndWork(Result result) => OnWorkDone?.Invoke(this, result);
    }

    public class EditFormVM<TModel> : EditFormVM where TModel : class, INotifyPropertyChanged, IEquatable<TModel>
    {
        // ReSharper disable InconsistentNaming
        protected TModel? _toEdit;
        protected TModel? _backup;
        // ReSharper restore InconsistentNaming

        protected EditFormVM(IWpfCommandsFactory commandsFactory) : base(commandsFactory)
        {
            CancelCommand = commandsFactory.Create(CancelExecute);
        }

        public virtual TModel? ToEdit
        {
            get => _toEdit;
            set
            {
                _ = Set(ref _toEdit, value);
                _backup ??= value.JsonSerializationDeepCopy();
            }
        }

        protected virtual void CancelExecute()
        {
            if (Equals(_backup, _toEdit))
            {
                EndWork(Result.Exit);
                return;
            }
            ToEdit = _backup.JsonSerializationDeepCopy();
        }

        protected override void EndWork(Result result)
        {
            base.EndWork(result);
            _backup = null;
        }
    }
}