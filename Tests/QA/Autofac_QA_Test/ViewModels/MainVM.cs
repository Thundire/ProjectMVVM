using Autofac_QA_Test.RegionsTests;

using Thundire.MVVM.Core.Observable;

namespace Autofac_QA_Test.ViewModels
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