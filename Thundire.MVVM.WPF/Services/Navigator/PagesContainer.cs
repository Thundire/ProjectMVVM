using System.Collections.Generic;
using System.Linq;
using Thundire.MVVM.WPF.Abstractions.DependencyInjection;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.Services.Navigator
{
    public class PagesContainer : IPagesContainer
    {
        private readonly IPagesInfoRegister _register;
        private readonly IDIContainer _container;

        public PagesContainer(IPagesInfoRegister register, IDIContainer container)
        {
            _register = register;
            _container = container;
        }

        public IDictionary<string, INavigablePage> GetPagesFromGroup(string group)
        {
            var groupInfo = _register.GetGroup(group);
            if (groupInfo.Count <= 0) return new Dictionary<string, INavigablePage>();
            return groupInfo
                .Where(info => _container.IsRegistered(info.PageType))
                .Select(info =>
                {
                    var page = _container.Resolve(info.PageType);
                    return (name: info.PageName, page: page as INavigablePage);
                })
                .ToDictionary(result => result.name, tuple => tuple.page);
        }

        public INavigablePage GetPage(string pageName)
        {
            var pageInfo = _register.GetPage(pageName);
            if (pageInfo is null) return null;
            return _container.Resolve(pageInfo.PageType) as INavigablePage;
        }
    }
}