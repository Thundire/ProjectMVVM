using System;
using System.Collections.Generic;

using Thundire.MVVM.WPF.Abstractions.DependencyInjection;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.Services.Navigator
{
    public class PagesRegistration : IPagesRegistration
    {
        private readonly IDIContainerBuilder _builder;

        public PagesRegistration(IDIContainerBuilder builder) => _builder = builder;

        private List<PageRegistrationInfo> Register { get; } = new();

        public void AddGroup(string groupName, Action<IPagesGroupRegistration> registration)
        {
            var group = new PagesGroupRegistration(groupName, _builder);
            registration.Invoke(group);
            foreach (var info in group.GetPages())
            {
                if (Exist(info.PageName, info.PageType)) throw new InvalidOperationException("Page already registered");
                Register.Add(info);
            }
        }

        public void AddPage<TPage>(string pageName)
        {
            var pageType = typeof(TPage);
            if (Exist(pageName, pageType)) throw new InvalidOperationException("Page already registered");
            _builder.RegisterType(pageType, LifeTimeMode.Transient);
            Register.Add(new() { PageName = pageName, PageType = typeof(TPage) });
        }

        public IPagesInfoRegister GetRegister() => new PagesInfoRegister(Register);

        private bool Exist(string pageName, Type pageType) =>
            Register.Exists(info => info.PageName == pageName || info.PageType == pageType);
    }
}