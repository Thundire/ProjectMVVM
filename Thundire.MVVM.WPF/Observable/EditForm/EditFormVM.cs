using System;
using System.ComponentModel;

using Thundire.Helpers;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Observable.EditForm
{
    public class EditFormVM : EditFormVMBase
    {
        protected EditFormVM(IWpfCommandsFactory commandsFactory)
        {
            CloseFormCommand = commandsFactory.Create(() => EndWork(Result.Exit));
        }

        public event EventHandler<Result>? OnWorkDone;

        protected virtual void EndWork(Result result) => OnWorkDone?.Invoke(this, result);
    }

    public class EditFormVM<TModel> : EditFormVMBase where TModel : class, INotifyPropertyChanged, IEquatable<TModel>
    {
        // ReSharper disable InconsistentNaming
        protected TModel? _toEdit;
        protected TModel? _backup;
        // ReSharper restore InconsistentNaming

        protected EditFormVM(IWpfCommandsFactory commandsFactory)
        {
            CancelCommand = commandsFactory.Create(CancelExecute);
            CloseFormCommand = commandsFactory.Create(() => EndWork(Result<TModel>.Exit));
        }

        public event EventHandler<Result<TModel>>? OnWorkDone;

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
                EndWork(Result<TModel>.Exit);
                return;
            }
            ToEdit = _backup.JsonSerializationDeepCopy();
        }

        protected void EndWork(Result<TModel> result)
        {
            OnWorkDone?.Invoke(this, result);
            _backup = null;
        }
    }
}