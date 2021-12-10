using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Thundire.MVVM.WPF.Commands
{
    public interface IWpfCommandsFactory
    {
        IWpfCommand Create(Action execute, Func<bool>? canExecute = null);
        IWpfCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null);
        IWpfCommand Create(Func<Task> execute, Func<bool>? canExecute = null);
        IWpfCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);

        ICommand CreateAsBase(Action execute, Func<bool>? canExecute = null);
        ICommand CreateAsBase<T>(Action<T> execute, Func<T, bool>? canExecute = null);
        ICommand CreateAsBase(Func<Task> execute, Func<bool>? canExecute = null);
        ICommand CreateAsBase<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);
    }
}