using System;
using System.Threading.Tasks;

namespace Thundire.MVVM.Core.Commands
{
    public interface IExecutableCommandsFactory : ICommandsFactory
    {
        new IExecutableCommand Create(Action execute, Func<bool>? canExecute = null);
        new IExecutableCommand Create<T>(Action<T?> execute, Func<T?, bool>? canExecute = null, bool parameterCanBeNull = false);
        new IExecutableCommand Create(Func<Task> execute, Func<bool>? canExecute = null);
        new IExecutableCommand Create<T>(Func<T?, Task> execute, Func<T?, bool>? canExecute = null, bool parameterCanBeNull = false);
    }
}