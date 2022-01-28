using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.ViewService
{
    public class ViewHandler : IViewOpener, IViewCloser
    {
        private readonly View? _viewCache;
        private readonly string? _mark;
        private readonly IViewsCache _viewsCache;
        private readonly Lazy<List<Func<View, View>>> _build = new();

        public ViewHandler(string mark, IViewsCache viewsCache)
        {
            _mark = mark;
            _viewsCache = viewsCache;
        }

        public ViewHandler(View viewCache, IViewsCache viewsCache)
        {
            _viewCache = viewCache;
            _viewsCache = viewsCache;
        }

        public IViewOpener WithOwner(object owner)
        {
            if (!_viewsCache.TryGetView(owner, out var view))
                throw new ArgumentException("Owner not supported");
            owner = view.CachedView;

            _build.Value.Add(v =>
            {
                if (v.CachedView is IWindowView window)
                {
                    window.SetOwner(owner);
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                return v;
            });

            return this;
        }

        public IViewOpener OnClose(CloseViewEventHandler onClose)
        {
            _build.Value.Add(view =>
            {
                view.OnClose = onClose;
                return view;
            });
            return this;
        }

        public IViewOpener ViewBehaviorOnLoaded<TView>(Action<TView> onLoaded)
        {
            _build.Value.Add(view =>
            {
                view.CachedView.Loaded += (sender, _) =>
                {
                    if (sender is not TView loadedView)
                    {
                        throw new InvalidOperationException($"View is not of type {typeof(TView).FullName}");
                    }
                    onLoaded.Invoke(loadedView);
                };
                return view;
            });
            return this;
        }

        public IViewOpener DataContextBehaviorOnLoaded<TDataContext>(Action<TDataContext> onLoaded)
        {
            _build.Value.Add(view =>
            {
                view.CachedView.Loaded += (sender, _) =>
                {
                    if (sender is not IView loadedView)
                    {
                        throw new InvalidOperationException($"View {sender.GetType().FullName} must inherit IView interface");
                    }

                    if (loadedView.DataContext is not TDataContext dataContext)
                    {
                        throw new InvalidOperationException($"View data context is not of type {typeof(TDataContext).FullName}");
                    }
                    onLoaded.Invoke(dataContext);
                };
                return view;
            });
            return this;
        }

        public IViewOpener NavigateOnLoaded(string pageKey)
        {
            _build.Value.Add(view =>
            {
                view.CachedView.Loaded += (sender, _) =>
                {
                    if (sender is not IView loadedView)
                    {
                        throw new InvalidOperationException($"View {sender.GetType().FullName} must inherit IView interface");
                    }

                    if (loadedView.DataContext is not INavigationRoot dataContext)
                    {
                        throw new InvalidOperationException($"View data context is not of type {typeof(INavigationRoot).FullName}");
                    }
                    dataContext.SetDefaultPage(pageKey);
                };
                return view;
            });
            return this;
        }

        public IViewOpener SetDataContext(object dataContext)
        {
            _build.Value.Add(view =>
            {
                view.CachedView.DataContext = dataContext;
                return view;
            });

            return this;
        }

        public TView? Handle<TView>() where TView : class
        {
            if(_mark is null) return null;

            var view = _viewsCache.Get(_mark);
            if (_build.IsValueCreated) _build.Value.Aggregate(view, (toAggregate, action) => action(toAggregate));
            return view.CachedView as TView;
        }

        public TView? Handle<TView>(object key) where TView : class
        {
            if(_mark is null) return null;

            var view = _viewsCache.Get(_mark, key);
            if (_build.IsValueCreated) _build.Value.Aggregate(view, (toAggregate, action) => action(toAggregate));
            return view.CachedView as TView;
        }

        public async ValueTask CloseSelf(CloseViewEventArgs args)
        {
            if(_viewCache is null) return;
            await _viewCache.HandleCloseSelf(args);
        }
    }
}