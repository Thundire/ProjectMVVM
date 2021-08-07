﻿using Autofac_QA_Test.RegionsTests.SinglePageRegionTest;
using Autofac_QA_Test.RegionsTests.StackViewsRegionTest;
using Thundire.MVVM.WPF.Observable.Base;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;

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