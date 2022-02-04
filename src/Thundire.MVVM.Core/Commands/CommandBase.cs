using System;

namespace Thundire.MVVM.Core.Commands
{
    public abstract class CommandBase : IExecutableCommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool Executable { get; set; }
        
        public virtual bool CanExecute(object? parameter) => Executable;
        public abstract void Execute(object? parameter);

        public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}