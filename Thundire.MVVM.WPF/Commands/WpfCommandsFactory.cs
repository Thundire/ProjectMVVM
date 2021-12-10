using System.Threading.Tasks;
using System;
using System.Windows.Input;
using Thundire.MVVM.Core.Commands;
using Thundire.MVVM.Core.Services;

namespace Thundire.MVVM.WPF.Commands
{
    public class WpfCommandsFactory : IWpfCommandsFactory
    {
        public IWpfCommand Create(Action execute, Func<bool>? canExecute = null) => new WpfRelayCommand(execute, canExecute);
        public IWpfCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null) => new WpfRelayCommand<T>(execute, canExecute);

        public IWpfCommand Create(Func<Task> execute, Func<bool>? canExecute = null) => new WpfAsyncCommand(execute, canExecute);
        public IWpfCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) => new WpfAsyncCommand<T>(execute, canExecute);

        public ICommand CreateBase(Action execute, Func<bool>? canExecute = null) => new WpfRelayCommand(execute, canExecute);
        public ICommand CreateBase<T>(Action<T> execute, Func<T, bool>? canExecute = null) => new WpfRelayCommand<T>(execute, canExecute);
        public ICommand CreateBase(Func<Task> execute, Func<bool>? canExecute = null)  => new WpfAsyncCommand(execute, canExecute);
        public ICommand CreateBase<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) => new WpfAsyncCommand<T>(execute, canExecute);

        IExecutableCommand ICommandsFactory.Create(Action execute, Func<bool>? canExecute) => Create(execute, canExecute);
        IExecutableCommand ICommandsFactory.Create<T>(Action<T> execute, Func<T, bool>? canExecute) => Create(execute, canExecute);
        IExecutableCommand ICommandsFactory.Create(Func<Task> execute, Func<bool>? canExecute) => Create(execute, canExecute);
        IExecutableCommand ICommandsFactory.Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute) => Create(execute, canExecute);
    }
}