namespace Thundire.MVVM.Core.Commands
{
    public abstract class CommandBase : IExecutableCommand
    {
        public virtual bool Executable { get; set; }

        bool IExecutableCommand.CanExecute(object? parameter) => Executable && CanExecute(parameter);
        void IExecutableCommand.Execute(object? parameter) => Execute(parameter);

        public virtual bool CanExecute(object? parameter) => true;
        public abstract void Execute(object? parameter);
    }
}