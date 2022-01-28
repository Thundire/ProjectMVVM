using System;
using System.Collections.Generic;
using System.Linq;

using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.PagesNavigator
{
    public class PagesGroupRegistration : IPagesGroupRegistration
    {
        private readonly string _name;

        public PagesGroupRegistration(string name) => _name = name;

        private Dictionary<string, Type> GroupRegister { get; } = new();

        public void Register<TPage>(string pageCallName) where TPage : class, INavigablePage
        {
            var page = typeof(TPage);

            if (GroupRegister.ContainsKey(pageCallName) && GroupRegister.ContainsValue(page))
            {
                throw new InvalidOperationException("Page already registered")
                {
                    Data =
                    {
                        ["GroupName"] = _name,
                        ["Type"] = page,
                        ["Key"] = pageCallName
                    }
                };
            }

            GroupRegister[pageCallName] = page;
        }

        public IEnumerable<PageRegistrationInfo> GetPages() => GroupRegister.Select(pair => new PageRegistrationInfo(pair.Key, _name, pair.Value));
    }
}