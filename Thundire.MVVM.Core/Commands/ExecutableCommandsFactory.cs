using System;
using System.Threading.Tasks;

namespace Thundire.MVVM.Core.Commands
{
    public class ExecutableCommandsFactory : CommandsFactory, IExecutableCommandsFactory
    {
        public new IExecutableCommand Create(Action execute, Func<bool>? canExecute = null) => new RelayCommand(execute, canExecute);
        public new IExecutableCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null) => new RelayCommand<T>(execute, canExecute);
        public new IExecutableCommand Create(Func<Task> execute, Func<bool>? canExecute = null) => new AsyncCommand(execute, canExecute);
        public new IExecutableCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) => new AsyncCommand<T>(execute, canExecute);
    }
}