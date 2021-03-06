using System;
using System.Threading;
using System.Threading.Tasks;

using Thundire.Helpers;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Abstractions.EditForms
{
    public abstract class EditCreateFormVM<TModel> : EditFormVM<TModel> where TModel : IEquatable<TModel>
    {
        private bool _isEditMode;

        protected EditCreateFormVM(IWpfCommandsFactory commandsFactory, IValidation<TModel> validation) : base(commandsFactory)
        {
            ConfirmCommand = commandsFactory.Create<TModel>(ConfirmExecute!, model => validation.Validate(model!) is SuccessResult);
            CancelCommand = commandsFactory.Create(CancelExecute);
            OnConfirm = ConfirmOnCreated;
        }

        public virtual bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value).DoOnSuccess(state =>
            {
                OnConfirm = state.New
                    ? ConfirmOnEdited
                    : ConfirmOnCreated;
            });
        }

        private Func<TModel, CancellationToken, Task<bool>>? OnConfirm { get; set; }

        protected virtual async Task ConfirmExecute(TModel edited)
        {
            if (OnConfirm is not null && await OnConfirm.Invoke(edited, CancellationToken.None))
            {
                EndWork(Result<TModel>.Ok(edited));
            }
        }

        protected abstract Task<bool> ConfirmOnCreated(TModel edited, CancellationToken token);

        protected abstract Task<bool> ConfirmOnEdited(TModel edited, CancellationToken token);
    }
}