﻿using System.Threading.Tasks;
using System.Windows.Input;
using Autofac_QA_Test.AppConfiguration;
using Thundire.Helpers;
using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Commands;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Autofac_QA_Test.ViewModels
{
    public class ViewOpenVM : NotifyBase
    {
        private readonly IViewHandlerService _viewHandlerService;
        private readonly ConfirmVM _confirmVM;
        private object ViewKeyGenerator = new object();

        public ViewOpenVM(IViewHandlerService viewHandlerService, IWpfCommandsFactory commandsFactory, ConfirmVM confirmVM)
        {
            _viewHandlerService = viewHandlerService;
            _confirmVM = confirmVM;
            _confirmVM.OnWorkDone += OnConfirmWorkDone;
            NumbersVM = new();

            OpenConfirmDialogCommand = commandsFactory.Create(OpenConfirmDialog);
            OpenNumbersEditCommand = commandsFactory.Create(OpenNumbersEditor);
        }

        private void OpenNumbersEditor()
        {
            var view = _viewHandlerService.Search(ViewsKeys.NumbersEditor).DataContextBehaviorOnLoaded<NumbersEditFormVM>(vm =>
            {
                vm.ToEdit = NumbersVM;
            }).WithOwner(MainWindowVM).OnClose(HandleNumbersEditorClose).Handle<IWindowView>();
            view?.Show();
        }

        private ValueTask HandleNumbersEditorClose(object? sender, CloseViewEventArgs args)
        {
            if (args is not CloseViewEventArgs<NumbersVM> { Result:{ } result }) return default;
            NumbersVM.Number1 = result.Number1;
            NumbersVM.Number2 = result.Number2;
            return default;
        }

        public NumbersVM NumbersVM { get; }
        public object MainWindowVM { get; internal set; }

        private void OpenConfirmDialog()
        {
           var view = _viewHandlerService.Search(ViewsKeys.Confirm).WithOwner(MainWindowVM).Handle<IWindowView>(ViewKeyGenerator);
           if(view is null) return;
           view.DataContext = _confirmVM;
           view.ShowDialog();
        }

        private void OnConfirmWorkDone(object? sender, Result e)
        {
            _viewHandlerService.Search(ViewKeyGenerator).CloseSelf(new CloseViewEventArgs());
        }

        public string ConfirmMessage 
        { 
            get => _confirmVM?.Message ?? default;
            set
            {
                if(_confirmVM is null) return;
                if(_confirmVM.Message == value) return;
                _confirmVM.Message = value;
                RaisePropertyChanged();
            }
        }

        public ICommand OpenConfirmDialogCommand { get; }
        public ICommand OpenNumbersEditCommand { get; }
    }
}