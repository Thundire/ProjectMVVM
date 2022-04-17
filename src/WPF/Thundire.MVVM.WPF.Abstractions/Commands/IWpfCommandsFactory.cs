using System;
using System.Threading.Tasks;

using Thundire.MVVM.Core.Commands;

namespace Thundire.MVVM.WPF.Abstractions.Commands
{
    public interface IWpfCommandsFactory : ICommandsFactory
    {
        new IWpfCommand Create(Action execute, Func<bool>? canExecute = null);
        new IWpfCommand Create<T>(Action<T?> execute, Func<T?, bool>? canExecute = null);
        new IWpfCommand Create(Func<Task> execute, Func<bool>? canExecute = null);
        new IWpfCommand Create<T>(Func<T?, Task> execute, Func<T?, bool>? canExecute = null);
    }
}