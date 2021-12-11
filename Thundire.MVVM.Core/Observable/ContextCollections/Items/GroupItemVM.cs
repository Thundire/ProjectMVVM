using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace Thundire.MVVM.Core.Observable.ContextCollections.Items
{
    public class GroupItemVM : ItemVM, IEnumerable<SingleItemVM>
    {
        protected GroupItemVM(IReadOnlyCollection<SingleItemVM>? items = null)
        {
            if (items is not null)
            {
                Inner = new ObservableCollection<SingleItemVM>(items);
                items.Select((item, index) =>
                {
                    item.Group = this;
                    item.Order = index;
                    return item;
                }).Execute();
            }

            Inner ??= new ObservableCollection<SingleItemVM>();
        }

        public bool HasSelectedItems => Inner.Any(i => i.IsSelected);
        public bool IsAllItemsSelected => Inner.All(i => i.IsSelected);

        public ObservableCollection<SingleItemVM> Inner { get; }

        public int Length => Inner.Count;

        public GroupItemVM AppendItem(SingleItemVM item)
        {
            Inner.Add(item);
            item.Group = this;
            item.Order = Inner.Count;
            return this;
        }

        public GroupItemVM AppendItems(IEnumerable<SingleItemVM> items)
        {
            foreach (var item in items) AppendItem(item);
            return this;
        }

        public GroupItemVM RemoveItem(SingleItemVM item)
        {
            Inner.Remove(item);
            item.Group = null;

            UpdateItemsOrder();

            return this;
        }

        protected override void HandleUpdateSelection(bool value)
        {
            base.HandleUpdateSelection(value);
            if (Inner.Count > 0 && Inner.Where(i => i.IsSelected != value).ToList() is { Count: > 0 } itemsToUpdate)
            {
                foreach (var i in itemsToUpdate) i.IsSelected = value;
            }
        }

        internal virtual void HandleInnerSelection(bool isInnerSelected)
        {
            if (isInnerSelected == _isSelected) return;

            if (isInnerSelected)
            {
                _isSelected = true;
                RaisePropertyChanged(nameof(IsSelected));
                return;
            }

            if (_isSelected && Inner.Count > 0 && Inner.Any(i => i.IsSelected)) return;

            _isSelected = false;
            RaisePropertyChanged(nameof(IsSelected));
        }

        protected virtual void UpdateItemsOrder()
        {
            var items = Inner.OrderBy(i => i.Order).ToImmutableArray();
            for (var i = 0; i < items.Length; i++)
            {
                items[i].Order = i;
            }
        }

        public IEnumerator<SingleItemVM> GetEnumerator() => Inner.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}