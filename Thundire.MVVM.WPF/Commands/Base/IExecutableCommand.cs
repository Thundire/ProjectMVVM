using System.Windows.Input;

namespace Thundire.MVVM.WPF.Commands.Base
{
    public interface IExecutableCommand : ICommand
    {
        bool Executable { get; set; }
    }
}