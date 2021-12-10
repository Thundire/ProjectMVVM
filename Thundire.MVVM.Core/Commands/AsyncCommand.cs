using System;
using System.Threading.Tasks;

namespace Thundire.MVVM.Core.Commands
{
    public class AsyncCommand : CommandBase
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;
        private readonly Action<Exception>? _onException;

        public AsyncCommand(Func<Task> execute, Func<bool>? canExecute = null, Action<Exception>? onException = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} cannot be null");
            _canExecute = canExecute;
            _onException = onException;
        }

        public override bool CanExecute(object? parameter) => base.CanExecute(parameter) && (_canExecute?.Invoke() ?? true);

        public override void Execute(object? parameter) => _execute().SafeFireAndForget(_onException);
    }

    public class AsyncCommand<TParameter> : CommandBase
    {
        private readonly Func<TParameter, Task> _execute;
        private readonly Func<TParameter, bool>? _canExecute;
        private readonly Action<Exception>? _onException;

        public AsyncCommand(Func<TParameter, Task> execute, Func<TParameter, bool>? canExecute = null, Action<Exception>? onException = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} cannot be null");
            _canExecute = canExecute ?? (_ => true);
            _onException = onException;
        }

        public override bool CanExecute(object? parameter) => base.CanExecute(parameter) && parameter is TParameter value && (_canExecute?.Invoke(value) ?? true);

        public override void Execute(object? parameter)
        {
            if (parameter is TParameter validParameter)
                _execute(validParameter).SafeFireAndForget(_onException);
        }
    }
}