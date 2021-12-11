using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Thundire.MVVM.WPF.Abstractions.ViewHandler;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    public partial class ViewHandlerService
    {
        public class ViewHandler : IViewOpener, IViewCloser
        {
            private readonly View _viewCache;
            private readonly string _mark;
            private readonly Lazy<List<Func<View, View>>> _build = new();

            public ViewHandler(string mark)
            {
                _mark = mark;
            }
            
            public ViewHandler(View viewCache)
            {
                _viewCache = viewCache;
            }

            public IViewOpener WithOwner(object owner)
            {
                if (!WindowsCache.TryGetValue(owner, out var view))
                    throw new ArgumentException("Owner not supported");
                owner = view.CachedView;

                _build.Value.Add(v =>
                {
                    if (v.CachedView is IWindowView window && owner is not null)
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

            public IViewOpener OnLoaded(Action<object> onLoaded)
            {
                _build.Value.Add(view =>
                {
                    view.CachedView.Loaded += (sender, args) => onLoaded((sender as IView)?.DataContext);
                    return view;
                });
                return this;
            }

            public TView Handle<TView>() where TView : class
            {
                var view = Get(_mark);
                if(_build.IsValueCreated) _build.Value.Aggregate(view, (toAggregate, action) => action(toAggregate));
                return view.CachedView as TView;
            }

            public async ValueTask CloseSelf(CloseViewEventArgs args)
            {
                await _viewCache.HandleCloseSelf(args);
            }
        }
    }
}