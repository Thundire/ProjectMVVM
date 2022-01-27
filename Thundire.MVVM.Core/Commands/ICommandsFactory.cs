using System.Threading.Tasks;
using System;
using System.Windows.Input;

namespace Thundire.MVVM.Core.Commands
{
    public interface ICommandsFactory
    {
        ICommand Create(Action execute, Func<bool>? canExecute = null);
        ICommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null);
        ICommand Create(Func<Task> execute, Func<bool>? canExecute = null);
        ICommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);
    }
}