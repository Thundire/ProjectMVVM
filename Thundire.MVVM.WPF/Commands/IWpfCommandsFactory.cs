using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Thundire.MVVM.Core.Services;

namespace Thundire.MVVM.WPF.Commands
{
    public interface IWpfCommandsFactory : ICommandsFactory
    {
        new IWpfCommand Create(Action execute, Func<bool>? canExecute = null);
        new IWpfCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null);
        new IWpfCommand Create(Func<Task> execute, Func<bool>? canExecute = null);
        new IWpfCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);

        ICommand CreateBase(Action execute, Func<bool>? canExecute = null);
        ICommand CreateBase<T>(Action<T> execute, Func<T, bool>? canExecute = null);
        ICommand CreateBase(Func<Task> execute, Func<bool>? canExecute = null);
        ICommand CreateBase<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);

    }
}