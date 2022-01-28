using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Regions;

namespace Shared.ViewModels.Regions
{
    public class RegionsMainVM : NotifyBase
    {
        public RegionsMainVM(IRegionsFactory regionsService,
                             SinglePageRegionTestMainVM singlePageRegionMain,
                             StackViewsRegionTestMainVM stackViewsRegionMain)
        {
            SinglePageRegion = regionsService.GetRegion(RegionsKeys.SinglePageRegion);
            StackViewsRegion = regionsService.GetRegion(RegionsKeys.StackViewsRegion);

            SinglePageRegionMain = singlePageRegionMain;
            StackViewsRegionMain = stackViewsRegionMain;
        }

        public SinglePageRegionTestMainVM SinglePageRegionMain { get; }
        public StackViewsRegionTestMainVM StackViewsRegionMain { get; }


        public IRegion SinglePageRegion { get; }
        public IRegion StackViewsRegion { get; }
    }
}