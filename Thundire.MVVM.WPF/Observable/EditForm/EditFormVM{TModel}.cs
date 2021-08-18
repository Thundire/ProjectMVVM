﻿using System;
using System.ComponentModel;
using Thundire.Helpers;
using Thundire.MVVM.WPF.Commands.Relay;

namespace Thundire.MVVM.WPF.Observable.EditForm
{
    public class EditFormVM<TModel> : EditFormVM where TModel : class, INotifyPropertyChanged, IEquatable<TModel>
    {
        // ReSharper disable InconsistentNaming
        protected TModel _toEdit;
        protected TModel _backup;
        // ReSharper restore InconsistentNaming

        protected EditFormVM()
        {
            CancelCommand = new RelayCommand(CancelExecute);
        }

        public virtual TModel ToEdit
        {
            get => _toEdit;
            set
            {
                _ = Set(ref _toEdit, value);
                _backup ??= value.DeepCopy();
            }
        }

        private void CancelExecute()
        {
            if (_backup is not null && _backup.Equals(_toEdit))
            {
                EndWork(new() { Result = false });
                return;
            }
            ToEdit = _backup.DeepCopy();
        }

        protected override void EndWork(EditFormResultArgs result)
        {
            base.EndWork(result);
            _backup = null;
        }
    }
}