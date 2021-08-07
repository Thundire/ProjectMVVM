using System.Collections.Generic;
using System.Windows.Navigation;
using Thundire.MVVM.WPF.Services.Navigator.Interfaces;

namespace Thundire.MVVM.WPF.Services.Navigator
{
    public class Navigator : INavigator, INavigationHandler
    {
        private readonly IPagesContainer _container;
        private NavigationService _navigator;

        public Navigator(IPagesContainer container)
        {
            _container = container;
            Pages = new Dictionary<string, INavigablePage>();
        }

        public NavigationService NavigationService
        {
            get => _navigator;
            set
            {
                if (_navigator is not null) _navigator.LoadCompleted -= OnLoadComplete;
                if(value is not null) value.LoadCompleted += OnLoadComplete;
                _navigator = value;
            }
        }

        private IDictionary<string, INavigablePage> Pages { get; set; }

        public string CurrentPage { get; private set; }

        public void UsePagesGroup(string group)
        {
            Pages = _container.GetPagesFromGroup(group);
        }

        public void NavigateTo(string pageName, object data)
        {
            if (!Pages.TryGetValue(pageName, out var page))
            {
                page = _container.GetPage(pageName);
                Pages.TryAdd(pageName, page);
            }

            if (NavigationService.Navigate(page, data)) CurrentPage = pageName;
        }

        private static void OnLoadComplete(object sender, NavigationEventArgs args)
        {
            if (args.Content is not INavigablePage page || args.ExtraData is not { } info) return;
            page.DataContext = info;
        }
    }
}