using Shared.ViewModels.Regions;
using Shared.ViewModels.ViewService;
using Thundire.MVVM.Core.Observable;

namespace Shared.ViewModels
{
    public class MainVM : NotifyBase
    {
        public MainVM(RegionsMainVM regionsMainVM, ViewOpenVM viewOpenVM, CommandsVM commandsVM)
        {
            RegionsMainVM = regionsMainVM;
            ViewOpenVM = viewOpenVM;
            CommandsVM = commandsVM;
            viewOpenVM.MainWindowVM = this;
        }
        public RegionsMainVM RegionsMainVM { get; }
        public ViewOpenVM ViewOpenVM { get; }
        public CommandsVM CommandsVM { get; }
    }
}