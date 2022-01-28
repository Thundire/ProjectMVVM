using Thundire.Helpers;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Abstractions.ViewService;
using Thundire.MVVM.WPF.Observable.EditForm;

namespace Shared.ViewModels.ViewService
{
    public class NumbersEditFormVM : EditFormVM<NumbersVM>
    {
        private readonly IViewHandlerService _viewHandlerService;

        public NumbersEditFormVM(IWpfCommandsFactory commandsFactory, IViewHandlerService viewHandlerService) : base(commandsFactory)
        {
            _viewHandlerService = viewHandlerService;
            ConfirmCommand = commandsFactory.Create(Confirm);
        }

        private void Confirm()
        {
            EndWork(Result<NumbersVM>.Ok(ToEdit));
        }

        protected override void EndWork(Result<NumbersVM> result)
        {
            base.EndWork(result);
            _viewHandlerService.Search(this).CloseSelf(new CloseViewEventArgs<NumbersVM>(dialogResult: true, result: ToEdit));
        }
    }
}