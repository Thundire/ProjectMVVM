using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Thundire.Core.DIContainer;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.ViewService
{
    public partial class ViewHandlerService : IViewHandlerService, IViewsCache
    {
        private static IDIContainer? _container;
        private static IReadOnlyList<ViewRegistration>? _registrations;
        private static readonly Dictionary<object, View> ViewsCache = new();
        
        public ViewHandlerService(IDIContainer container, IViewRegisterCache cache)
        {
            _container ??= container;
            _registrations ??= cache.Registrations;
        }

        public IViewOpener Search(string mark) => new ViewHandler(mark, this);
        public IViewCloser Search(object key)
        {
            if (!ViewsCache.TryGetValue(key, out var view))
                throw new ArgumentException("Connector not supported or view already closed", nameof(key))
                {
                    Data =
                    {
                        [nameof(key)] = key
                    }
                };
            return new ViewHandler(view!, this);
        }

        public bool TryGetView(object owner,[NotNullWhen(true)] out View? view)
        {
            return ViewsCache.TryGetValue(owner, out view);
        }

        public View Get(string mark)
        {
            if (_registrations is null || _container is null) throw new InvalidOperationException("View service was not correctly initialized");
            if (_registrations.FirstOrDefault(r => r.Mark == mark) is not { } registration) throw new InvalidOperationException("Mark was not registered");

            var view = _container.Resolve(registration.View);
            if (view is not IView subscriber)
                throw new InvalidOperationException($"Invalid registration of view marked as {mark}");

            if (!registration.HasViewModel)
                return new View(subscriber, mark);

            var viewModel = _container.Resolve(registration.ViewModel!);
            subscriber.DataContext = viewModel;
            
            return RegisterInCache(viewModel, subscriber);
        }

        public View Get(string mark, object key)
        {
            if (_registrations is null || _container is null) throw new InvalidOperationException("View service was not correctly initialized");
            if (_registrations.FirstOrDefault(r => r.Mark == mark) is not { } registration) throw new InvalidOperationException("Mark was not registered");

            var view = _container.Resolve(registration.View);
            if (view is not IView subscriber)
                throw new InvalidOperationException($"Invalid registration of view marked as {mark}");
            
            if (!registration.HasViewModel)
                return RegisterInCache(key, subscriber);

            var viewModel = _container.Resolve(registration.ViewModel!);
            subscriber.DataContext = viewModel;
            
            return RegisterInCache(viewModel, subscriber);
        }

        private static View RegisterInCache<TView>(object key, TView view) where TView : class, IView
        {
            var cache = new View(view, key);
            cache.OnUnsubscribeFromCache += UnsubscribeFromCache;
            ViewsCache[key] = cache;
            return cache;
        }

        private static void UnsubscribeFromCache(object viewModel)
        {
            ViewsCache.Remove(viewModel);
        }
    }
}