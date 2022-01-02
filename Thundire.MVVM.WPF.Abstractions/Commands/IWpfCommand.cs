using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Thundire.MVVM.Core.Commands;

namespace Thundire.MVVM.WPF.Abstractions.Commands
{
    public interface IWpfCommand : IExecutableCommand
    {
        public void RaiseCanExecuteChanged()
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                CommandManager.InvalidateRequerySuggested();
                return;
            }
            Application.Current.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested, DispatcherPriority.Send);
        }
    }
}