using System;

namespace Thundire.MVVM.Core.Commands
{
    public class RelayCommand<TParameter> : CommandBase
    {
        private readonly Action<TParameter> _execute;
        private readonly Func<TParameter, bool>? _canExecute;

        public RelayCommand(Action<TParameter> execute, Func<TParameter, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} cannot be null");
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter) => base.CanExecute(parameter) && parameter is TParameter value && (_canExecute?.Invoke(value) ?? true);

        public override void Execute(object? parameter)
        {
            if(parameter is TParameter value)
                _execute.Invoke(value);
        }
    }

    public class RelayCommand : CommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} cannot be null");
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter) => base.CanExecute(parameter) && (_canExecute?.Invoke() ?? true);

        public override void Execute(object? parameter) => _execute.Invoke();
    }
}