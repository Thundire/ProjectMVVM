using System;
using Thundire.MVVM.WPF.Commands.Base;

namespace Thundire.MVVM.WPF.Commands.Relay
{
    public sealed class RelayCommand<TParameter> : CommandBase
    {
        private readonly Action<TParameter> _execute;
        private readonly Func<TParameter, bool> _canExecute;

        public RelayCommand(Action<TParameter> execute, Func<TParameter, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute is null || _canExecute.Invoke((TParameter)parameter);

        public override void Execute(object parameter) => _execute.Invoke((TParameter)parameter);
    }
}