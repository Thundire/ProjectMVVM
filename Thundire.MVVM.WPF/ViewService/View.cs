using System;
using System.Threading.Tasks;
using System.Windows;

using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.ViewService
{
    public class View
    {
        private readonly object _key;
        private readonly IView _cachedView;
        private readonly object? _dataContext;

        private bool _isCloseHandled;

        public View(IView view, object key, object? dataContext)
        {
            _key = key;
            _dataContext = dataContext;
            _cachedView = view;
            if (view is IWindowView window)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Closed += WindowOnClosed;
            }
        }

        public IView CachedView => _cachedView;

        public event Action<View, object>? OnUnsubscribeFromCache;
        public CloseViewEventHandler? OnClose { get; set; }

        public async ValueTask HandleCloseSelf(CloseViewEventArgs args)
        {
            if (_isCloseHandled) return;
            _isCloseHandled = true;
            _cachedView.Close();
            if (OnClose is not null)
            {
                await OnClose.Invoke(_dataContext, args);
            }
            OnUnsubscribeFromCache?.Invoke(this, _key);
        }

        private async void WindowOnClosed(object? sender, EventArgs e)
        {
            if (_isCloseHandled) return;
            _isCloseHandled = true;
            _cachedView.Close();
            if (OnClose is not null)
            {
                await OnClose.Invoke(_dataContext, new(e));
            }

            if (sender is IWindowView window)
            {
                window.Closed -= WindowOnClosed;
            }
            OnUnsubscribeFromCache?.Invoke(this, _key);
        }
    }
}