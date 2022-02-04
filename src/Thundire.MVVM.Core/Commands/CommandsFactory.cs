using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Thundire.MVVM.Core.Commands
{
    public class CommandsFactory : ICommandsFactory
    {
        public ICommand Create(Action execute, Func<bool>? canExecute = null) => new RelayCommand(execute, canExecute);
        public ICommand Create<T>(Action<T?> execute, Func<T?, bool>? canExecute = null, bool parameterCanBeNull = false) =>
            new RelayCommand<T?>(execute, canExecute) { IsParameterCanBeNull = parameterCanBeNull };
        public ICommand Create(Func<Task> execute, Func<bool>? canExecute = null) => new AsyncCommand(execute, canExecute);
        public ICommand Create<T>(Func<T?, Task> execute, Func<T?, bool>? canExecute = null, bool parameterCanBeNull = false) =>
            new AsyncCommand<T?>(execute, canExecute) { IsParameterCanBeNull = parameterCanBeNull };
    }
}