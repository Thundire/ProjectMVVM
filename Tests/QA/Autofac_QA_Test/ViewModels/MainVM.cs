﻿using Autofac_QA_Test.RegionsTests;
using Thundire.MVVM.WPF.Observable.Base;

namespace Autofac_QA_Test.ViewModels
{
    public class MainVM : NotifyBase
    {
        public MainVM(RegionsMainVM regionsMainVM)
        {
            RegionsMainVM = regionsMainVM;
        }
        public RegionsMainVM RegionsMainVM { get; }
    }
}