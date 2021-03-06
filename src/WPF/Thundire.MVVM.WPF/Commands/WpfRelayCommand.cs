using System;
using System.Windows.Input;

using Thundire.MVVM.Core.Commands;
using Thundire.MVVM.WPF.Abstractions;
using Thundire.MVVM.WPF.Abstractions.Commands;

namespace Thundire.MVVM.WPF.Commands
{
    public class WpfRelayCommand : RelayCommand, IWpfCommand
    {
        private bool _executable = true;

        public WpfRelayCommand(Action execute, Func<bool>? canExecute = null) : base(execute, canExecute)
        {
        }

        public override bool Executable { get => _executable; set => this.Set(ref _executable, value, true); }

        public new event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class WpfRelayCommand<TParameter> : RelayCommand<TParameter>, IWpfCommand
    {
        private bool _executable = true;

        public WpfRelayCommand(Action<TParameter?> execute, Func<TParameter?, bool>? canExecute = null) : base(execute, canExecute)
        {
        }

        public override bool Executable { get => _executable; set => this.Set(ref _executable, value, true); }

        public new event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}