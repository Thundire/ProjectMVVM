using System.Collections.Generic;
using System.Windows.Navigation;
using Thundire.MVVM.Core.PagesNavigator;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.PagesNavigator
{
    public class Navigator : INavigator, INavigationHandler
    {
        private readonly IPagesContainer _container;
        private NavigationService? _navigator;

        public Navigator(IPagesContainer container)
        {
            _container = container;
            Pages = new Dictionary<string, INavigablePage>();
        }

        public NavigationService? NavigationService
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

        public string? CurrentPageKey { get; private set; }
        public object? CurrentDataContext { get; private set; }
        private INavigablePage? CurrentPage { get; set; }
        

        public void UsePagesGroup(string group)
        {
            Pages = _container.GetPagesFromGroup(group);
        }

        public void NavigateTo(string pageName, object dataContext)
        {
            INavigablePage? nextPage = null;
            if (Pages.TryGetValue(pageName, out var page))
            {
                nextPage = page;
            }

            if (nextPage is null && _container.GetPage(pageName) is { } containerPage)
            {
                nextPage = containerPage;
                Pages.TryAdd(pageName, containerPage);
            }

            if (NavigationService?.Navigate(nextPage, dataContext) is true)
            {
                CurrentPage = nextPage;
                CurrentPageKey = pageName;
                CurrentDataContext = dataContext;
            }
        }

        public void ChangeDataContextOfCurrentPage(object dataContext)
        {
            if(CurrentPage is null) return;
            CurrentPage.DataContext = dataContext;
        }

        private static void OnLoadComplete(object sender, NavigationEventArgs args)
        {
            if (args.Content is not INavigablePage page || args.ExtraData is not { } dataContext) return;
            page.DataContext = dataContext;
        }
    }
}