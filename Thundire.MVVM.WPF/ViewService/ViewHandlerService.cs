using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.ViewService
{
    public class ViewHandlerService : IViewHandlerService, IViewsCache
    {
        private readonly IViewRegisterCache _registeredViewsCache;
        private static readonly Dictionary<object, View> ViewsCache = new();

        public ViewHandlerService(IViewRegisterCache registeredViewsCache)
        {
            _registeredViewsCache = registeredViewsCache;
        }

        public int CachedLength => ViewsCache.Count;

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

        public bool TryGetView(object owner, [NotNullWhen(true)] out View? view)
        {
            return ViewsCache.TryGetValue(owner, out view);
        }

        public View Get(string mark)
        {
            if (_registeredViewsCache.IsMarkNotRegistered(mark)) throw new InvalidOperationException("Mark was not registered");

            var viewDescriptor = _registeredViewsCache.GetView(mark);

            if (!viewDescriptor.HasDataContext) return RegisterInCache(mark, viewDescriptor.View);

            return RegisterInCache(viewDescriptor.DataContext!, viewDescriptor.View, viewDescriptor.DataContext);
        }

        public View Get(string mark, object key)
        {
            if (_registeredViewsCache.IsMarkNotRegistered(mark)) throw new InvalidOperationException("Mark was not registered");

            var viewDescriptor = _registeredViewsCache.GetView(mark);

            return RegisterInCache(key, viewDescriptor.View, viewDescriptor.DataContext);
        }

        private static View RegisterInCache(object key, IView view, object? dataContext = null)
        {
            var cache = new View(view, key, dataContext);
            cache.OnUnsubscribeFromCache += UnsubscribeFromCache;
            ViewsCache[key] = cache;
            return cache;
        }

        private static void UnsubscribeFromCache(View sender, object key)
        {
            ViewsCache.Remove(key);
            sender.OnUnsubscribeFromCache -= UnsubscribeFromCache;
        }
    }
}