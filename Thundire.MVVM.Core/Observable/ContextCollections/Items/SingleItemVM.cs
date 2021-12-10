namespace Thundire.MVVM.Core.Observable.ContextCollections.Items
{
    public class SingleItemVM : ItemVM
    {
        private GroupItemVM? _group;
        public GroupItemVM? Group { get => _group; set => Set(ref _group, value); }

        protected override void HandleUpdateSelection(bool value)
        {
            base.HandleUpdateSelection(value);
            Group?.HandleInnerSelection(value);
        }
    }
}