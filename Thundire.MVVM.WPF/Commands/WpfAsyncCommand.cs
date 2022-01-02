using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Thundire.MVVM.Core.Commands;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Commands
{
    public class WpfAsyncCommand<TParameter> : AsyncCommand<TParameter>, IWpfCommand, ICommand
    {
        private bool _executable = true;

        public WpfAsyncCommand(Func<TParameter, Task> execute, Func<TParameter, bool>? canExecute = null, Action<Exception>? onException = null)
            : base(execute, canExecute, onException)
        {
        }

        public override bool Executable { get => _executable; set => this.Set(ref _executable, value, true); }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class WpfAsyncCommand : AsyncCommand, IWpfCommand, ICommand
    {
        private bool _executable = true;

        public WpfAsyncCommand(Func<Task> execute, Func<bool>? canExecute = null, Action<Exception>? onException = null) : base(execute, canExecute, onException)
        {
        }

        public override bool Executable { get => _executable; set => this.Set(ref _executable, value, true); }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}