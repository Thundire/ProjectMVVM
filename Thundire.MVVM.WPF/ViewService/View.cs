using System;
using System.Threading.Tasks;
using System.Windows;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.ViewService
{
    public class View
    {
        private readonly string? _mark;
        private readonly object? _key;
        private readonly IView _cachedView;

        private bool _isCloseHandled;

        public View(IView view, string mark) : this(view)
        {
            _mark = mark;
        }

        public View(IView view, object viewModel): this(view)
        {
            _key = viewModel;
        }

        private View(IView view)
        {
            _cachedView = view;
            if (view is IWindowView window)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Closed += WindowOnClosed;
            }
        }

        public IView CachedView => _cachedView;

        public event Action<object>? OnUnsubscribeFromCache;
        public CloseViewEventHandler? OnClose { get; set; }

        public async ValueTask HandleCloseSelf(CloseViewEventArgs args)
        {
            if (_isCloseHandled) return;
            _isCloseHandled = true;
            _cachedView.Close();
            if (OnClose is not null)
                await OnClose.Invoke(_key, args);
            if(_key is not null) OnUnsubscribeFromCache?.Invoke(_key);
        }
        private async void WindowOnClosed(object? sender, EventArgs e)
        {
            if (_isCloseHandled) return;
            _isCloseHandled = true;
            _cachedView.Close();
            if (OnClose is not null)
                await OnClose.Invoke(sender, new(e));
            if (_key is not null) OnUnsubscribeFromCache?.Invoke(_key);
        }
    }
}