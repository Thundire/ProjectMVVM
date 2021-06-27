using System;
using System.Collections.Generic;
using System.Linq;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.Navigator.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.Navigator
{
    public class PagesGroupRegistration : IPagesGroupRegistration
    {
        private readonly string _name;
        private readonly IDIContainerBuilder _builder;

        public PagesGroupRegistration(string name, IDIContainerBuilder builder)
        {
            _name = name;
            _builder = builder;
        }

        private Dictionary<string, Type> GroupRegister { get; } = new();

        public void Register<TPage>(string pageCallName) where TPage : class, INavigablePage
        {
            var page = typeof(TPage);
            _builder.RegisterType(page, LifeTimeMode.Transient);
            GroupRegister[pageCallName] = page;
        }

        public IEnumerable<PageRegistrationInfo> GetPages() => GroupRegister.Select(pair => new PageRegistrationInfo()
        {
            GroupName = _name,
            PageName = pair.Key,
            PageType = pair.Value
        });
    }
}