using System;
using System.Threading.Tasks;
using Thundire.MVVM.Core.Commands;

namespace Thundire.MVVM.Core.Services
{
    public interface ICommandsFactory
    {
       IExecutableCommand Create(Action execute, Func<bool>? canExecute = null);
       IExecutableCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null);
       IExecutableCommand Create(Func<Task> execute, Func<bool>? canExecute = null);
       IExecutableCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);
    }
}