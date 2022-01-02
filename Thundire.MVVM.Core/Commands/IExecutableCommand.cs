namespace Thundire.MVVM.Core.Commands
{
    public interface IExecutableCommand
    {
        bool Executable { get; set; }

        bool CanExecute(object? parameter);
        void Execute(object? parameter);
    }
}