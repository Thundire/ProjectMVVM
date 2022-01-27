using System;
using System.Threading.Tasks;

using Thundire.MVVM.Core.Commands;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Commands
{
    public class WpfCommandsFactory : ExecutableCommandsFactory, IWpfCommandsFactory
    {
        public new IWpfCommand Create(Action execute, Func<bool>? canExecute = null) => new WpfRelayCommand(execute, canExecute);
        public new IWpfCommand Create<T>(Action<T> execute, Func<T, bool>? canExecute = null) => new WpfRelayCommand<T>(execute, canExecute);
        public new IWpfCommand Create(Func<Task> execute, Func<bool>? canExecute = null) => new WpfAsyncCommand(execute, canExecute);
        public new IWpfCommand Create<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null) => new WpfAsyncCommand<T>(execute, canExecute);
    }
}