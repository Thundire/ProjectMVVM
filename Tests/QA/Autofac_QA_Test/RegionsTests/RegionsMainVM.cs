using Autofac_QA_Test.RegionsTests.SinglePageRegionTest;
using Autofac_QA_Test.RegionsTests.StackViewsRegionTest;

using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Regions;

namespace Autofac_QA_Test.RegionsTests
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