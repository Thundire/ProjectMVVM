using System;
using System.Threading.Tasks;
using Thundire.Helpers;

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
        private readonly Func<TParameter?, Task> _execute;
        private readonly Func<TParameter?, bool>? _canExecute;
        private readonly Action<Exception>? _onException;

        public AsyncCommand(Func<TParameter?, Task> execute, Func<TParameter?, bool>? canExecute = null, Action<Exception>? onException = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} cannot be null");
            _canExecute = canExecute ?? (_ => true);
            _onException = onException;
        }

        public bool IsParameterCanBeNull { get; init; }

        public override bool CanExecute(object? parameter)
        {
            if (!base.CanExecute(parameter)) return false;
            if (!IsParameterCanBeNull && parameter is not TParameter) return false;
            return _canExecute?.Invoke((TParameter?)parameter) ?? true;
        }

        public override void Execute(object? parameter)
        {
            if (!IsParameterCanBeNull && parameter is TParameter value)
            {
                _execute.Invoke(value).SafeFireAndForget(_onException);
                return;
            }

            _execute.Invoke((TParameter?)parameter).SafeFireAndForget(_onException);
        }
    }
}