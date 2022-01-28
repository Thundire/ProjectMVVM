using Shared.ViewModels.Regions;
using Shared.ViewModels.ViewService;
using Thundire.MVVM.Core.Observable;

namespace Shared.ViewModels
{
    public class MainVM : NotifyBase
    {
        public MainVM(RegionsMainVM regionsMainVM, ViewOpenVM viewOpenVM)
        {
            RegionsMainVM = regionsMainVM;
            ViewOpenVM = viewOpenVM;
            viewOpenVM.MainWindowVM = this;
        }
        public RegionsMainVM RegionsMainVM { get; }
        public ViewOpenVM ViewOpenVM { get; }
    }
}