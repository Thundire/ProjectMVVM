using System.Windows.Input;

namespace Thundire.MVVM.Core.Commands
{
    public interface IExecutableCommand : ICommand
    {
        bool Executable { get; set; }

        void NotifyCanExecuteChanged();
    }
}