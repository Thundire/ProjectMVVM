using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Observable.EditForm;

namespace Shared.ViewModels.ViewService
{
    public class ConfirmVM : EditFormVM
    {
        private string _message;

        public ConfirmVM(IWpfCommandsFactory commandsFactory) : base(commandsFactory)
        {
        }

        public string Message { get => _message; set => Set(ref _message, value); }
    }
}