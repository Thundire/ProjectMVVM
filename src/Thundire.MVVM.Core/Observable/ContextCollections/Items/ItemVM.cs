namespace Thundire.MVVM.Core.Observable.ContextCollections.Items
{
    public abstract class ItemVM : NotifyBase
    {
        private int _order;
        // ReSharper disable once InconsistentNaming
        protected bool _isSelected;
        private ContextCommands _contextCommands;

        protected ItemVM() => _contextCommands = ContextCommands.Empty();

        public int Order { get => _order; set => Set(ref _order, value); }
        public bool IsSelected { get => _isSelected; set => HandleUpdateSelection(value); }
        public ContextCommands ContextCommands { get => _contextCommands; set => Set(ref _contextCommands, value); }

        protected virtual void HandleUpdateSelection(bool value) => Set(ref _isSelected, value, nameof(IsSelected));
    }
}