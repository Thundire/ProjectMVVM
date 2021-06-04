using System;
using System.Threading.Tasks;
using System.Windows;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;

namespace Thundire.MVVM.WPF.Services.ViewService.Models
{
    public class View
    {
        private readonly object _key;
        private readonly IView _cachedView;

        private bool _isCloseHandled;

        public View(object viewModel)
        {
            _key = viewModel;
        }

        public IView CachedView
        {
            get => _cachedView;
            init
            {
                _cachedView = value;
                if (value is IWindowView window)
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.Closed += WindowOnClosed;
                }
            }
        }

        public event Action<object> OnUnsubscribeFromCache;
        public CloseViewEventHandler OnClose { get; set; }

        public async ValueTask HandleCloseSelf(CloseViewEventArgs args)
        {
            if(_isCloseHandled) return;
            _isCloseHandled = true;
            _cachedView.Close();
            if(OnClose is not null)
                await OnClose.Invoke(_key, args);
            OnUnsubscribeFromCache?.Invoke(_key);
        }
        private async void WindowOnClosed(object sender, EventArgs e)
        {
            if (_isCloseHandled) return;
            _isCloseHandled = true;
            _cachedView.Close();
            if (OnClose is not null)
                await OnClose.Invoke(sender, new(e));
            OnUnsubscribeFromCache?.Invoke(_key);
        }
    }
}