using System;
using System.Collections.Generic;
using Thundire.Core.DIContainer;
using Thundire.MVVM.WPF.Core.DICache.PagesNavigator;

namespace Thundire.MVVM.WPF.DICache.PagesNavigator
{
    public class PagesRegistration : IPagesRegistration
    {
        private readonly IDIContainerBuilder _builder;

        public PagesRegistration(IDIContainerBuilder builder)
        {
            _builder = builder;
            _registeredPages = new HashSet<Type>();
            Register = new HashSet<PageRegistrationInfo>();
        }

        private HashSet<PageRegistrationInfo> Register { get; }
        private readonly HashSet<Type> _registeredPages;

        public void AddGroup(string groupName, Action<IPagesGroupRegistration> registration)
        {
            var group = new PagesGroupRegistration(groupName);
            registration.Invoke(group);
            foreach (var info in group.GetPages())
            {
                _registeredPages.Add(info.PageType);
                Register.Add(info);
            }
        }

        public void AddPage<TPage>(string pageName)
        {
            var pageType = typeof(TPage);
            _registeredPages.Add(pageType);
            Register.Add(new(pageName, typeof(TPage)));
        }

        public IPagesInfoRegister GetRegister()
        {
            foreach (var page in _registeredPages)
            {
                _builder.RegisterType(page, LifeTimeMode.Transient);
            }

            return new PagesInfoRegister(Register);
        }
    }
}