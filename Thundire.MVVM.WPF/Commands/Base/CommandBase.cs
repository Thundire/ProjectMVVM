using System;
using System.Windows.Input;

namespace Thundire.MVVM.WPF.Commands.Base
{
    public abstract class CommandBase : IExecutableCommand
    {
        private bool _executable = true;
        
        public bool Executable
        {
            get => _executable;
            set
            {
                if (_executable == value) return;
                _executable = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => _executable && CanExecute(parameter);

        void ICommand.Execute(object parameter)
        {
            if (CanExecute(parameter)) Execute(parameter);
        }

        public virtual bool CanExecute(object parameter) => true;

        public abstract void Execute(object parameter);
    }
}