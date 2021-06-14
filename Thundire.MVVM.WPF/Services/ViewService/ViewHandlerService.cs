using System;
using System.Collections.Generic;
using System.Linq;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    public partial class ViewHandlerService : IViewHandlerService
    {
        private static IDIContainer _container;
        private static IReadOnlyList<ViewRegistration> _registrations;
        private static readonly Dictionary<object, View> WindowsCache = new();
        
        public ViewHandlerService(IDIContainer container, IViewRegisterCache cache)
        {
            _container = container;
            _registrations = cache.Registrations;
        }

        public IViewOpener Search(string mark) => new ViewHandler(mark);
        public IViewCloser Search(object connector)
        {
            if (!WindowsCache.TryGetValue(connector, out var view))
                throw new ArgumentException("Connector not supported");
            return new ViewHandler(view);
        }

        private static View Get(string mark)
        {
            if (_registrations.FirstOrDefault(r => r.Mark == mark) is not { } registration) return null;
            if (!_container.IsRegistered(registration.View)) return null;

            var view = _container.Resolve(registration.View);
            if (view is not IView subscriber)
                throw new InvalidOperationException($"Invalid registration of view marked as {mark}");

            if (!registration.ViewModelRequired || !_container.IsRegistered(registration.ViewModel))
                return new View(null) {CachedView = subscriber};

            var viewModel = _container.Resolve(registration.ViewModel);
            subscriber.DataContext = viewModel;
            
            return RegisterInCache(viewModel, subscriber);
        }
        private static View RegisterInCache<TView>(object viewModel, TView view) where TView : class, IView
        {
            var cache = new View(viewModel) { CachedView = view };
            cache.OnUnsubscribeFromCache += UnsubscribeFromCache;
            WindowsCache[viewModel] = cache;
            return cache;
        }
        private static void UnsubscribeFromCache(object viewModel)
        {
            WindowsCache.Remove(viewModel);
        }
    }
}