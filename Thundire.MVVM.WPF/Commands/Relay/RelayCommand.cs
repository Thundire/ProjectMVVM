﻿using System;
using Thundire.MVVM.WPF.Commands.Base;

namespace Thundire.MVVM.WPF.Commands.Relay
{
    public sealed class RelayCommand : CommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public override void Execute(object parameter) => _execute?.Invoke();
    }
}