using System;

using Thundire.Helpers;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Abstractions.EditForms
{
    public class EditFormVM : EditFormVMBase
    {
        protected EditFormVM(IWpfCommandsFactory commandsFactory)
        {
            ConfirmCommand = commandsFactory.Create(Confirm);
            CloseFormCommand = commandsFactory.Create(() => EndWork(Result.Exit));
            CancelCommand = commandsFactory.Create(Cancel);
        }

        public event EventHandler<Result>? OnWorkDone;

        protected virtual void Confirm() => EndWork(Result.Ok());
        protected virtual void Cancel() => EndWork(Result.Exit);

        protected virtual void EndWork(Result result) => OnWorkDone?.Invoke(this, result);
    }

    public class EditFormVM<TModel> : EditFormVMBase where TModel : IEquatable<TModel>
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

        public TModel? DefaultBackupValue { private get; set; } = default;
        public bool IsBackupEnabled { private get; set; } = true;

        public virtual TModel? ToEdit
        {
            get => _toEdit;
            set
            {
                _ = Set(ref _toEdit, value);
                if(IsBackupEnabled) _backup ??= value.JsonSerializationDeepCopy();
            }
        }

        protected virtual void CancelExecute()
        {
            if (!IsBackupEnabled)
            {
                EndWork(Result<TModel>.Exit);
                return;
            }

            if (Equals(_backup, _toEdit))
            {
                EndWork(Result<TModel>.Exit);
                return;
            }
            ToEdit = _backup.JsonSerializationDeepCopy();
        }

        protected virtual void EndWork(Result<TModel> result)
        {
            OnWorkDone?.Invoke(this, result);
            if (IsBackupEnabled) _backup = DefaultBackupValue;
        }
    }
}