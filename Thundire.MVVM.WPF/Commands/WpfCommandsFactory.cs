using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Commands
{
    public class WpfCommandsFactory : IWpfCommandsFactory
    {
        public IWpfCommand Create(Action execute, Func<bool>? canExecute = null) => new WpfRelayCommand(execute, canExecute);
        public IWpfCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null) => new WpfRelayCommand<T>(execute, canExecute);

        public IWpfCommand Create(Func<Task> execute, Func<bool>? canExecute = null) => new WpfAsyncCommand(execute, canExecute);
        public IWpfCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) => new WpfAsyncCommand<T>(execute, canExecute);

        public ICommand CreateAsBase(Action execute, Func<bool>? canExecute = null) => new WpfRelayCommand(execute, canExecute);
        public ICommand CreateAsBase<T>(Action<T> execute, Func<T, bool>? canExecute = null) => new WpfRelayCommand<T>(execute, canExecute);
        public ICommand CreateAsBase(Func<Task> execute, Func<bool>? canExecute = null) => new WpfAsyncCommand(execute, canExecute);
        public ICommand CreateAsBase<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) => new WpfAsyncCommand<T>(execute, canExecute);
    }
}