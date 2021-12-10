using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Immutable;
using System.Linq;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.Core.Observable.ContextCollections
{
    public class ItemsContext<TSingleItem> : NotifyBase where TSingleItem : SingleItemVM
    {
        private Func<ContextCommands>? _itemContextCommands;
        protected bool IsClearingItems;

        public ItemsContext(IReadOnlyList<ItemVM>? items = null)
        {
            if (items is not null)
            {
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    item.PropertyChanged += ItemOnPropertyChanged;
                    item.Order = i;
                }

                Collection = new ObservableCollection<ItemVM>(items);
            }

            Collection ??= new ObservableCollection<ItemVM>();
        }

        protected virtual void ItemOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                RaisePropertyChanged(nameof(HasSelectedItems));
                OnItemsSelectionChanged?.Invoke(HasSelectedItems);
            }
        }
        
        public ObservableCollection<ItemVM> Collection { get; }

        public Func<ContextCommands>? ItemContextCommands
        {
            get => _itemContextCommands;
            set
            {
                _itemContextCommands = value;
                if (Collection.Count <= 0) return;
                var items = GetExistedItems();

                if (value is not null)
                {
                    foreach (var item in items)
                    {
                        item.ContextCommands = value.Invoke();
                    }

                    return;
                }

                foreach (var item in items)
                {
                    item.ContextCommands = ContextCommands.Empty();
                }
            }
        }

        protected virtual IReadOnlyCollection<TSingleItem> GetExistedItems()
        {
            return Collection.OfType<TSingleItem>().ToList();
        }

        public bool HasSelectedItems => Collection.Any(i => i.IsSelected);
        public event Action<bool>? OnItemsSelectionChanged;

        public void Add(TSingleItem item)
        {
            item.Order = Collection.Count;
            Collection.Add(item);
            if (_itemContextCommands is not null) item.ContextCommands = _itemContextCommands.Invoke();
            item.PropertyChanged += ItemOnPropertyChanged;
        }

        public void DeselectAll()
        {
            foreach (var item in Collection.Where(i => i.IsSelected))
            {
                item.IsSelected = false;
            }
        }

        public virtual void RemoveAll()
        {
            IsClearingItems = true;
            if (HasSelectedItems)
            {
                RemoveSelected();

                IsClearingItems = false;
                return;
            }

            ClearAll();
            IsClearingItems = false;
        }

        protected virtual void RemoveSelected()
        {
            foreach (var item in Collection.Where(i => i.IsSelected).ToList())
            {
                Collection.Remove(item);
                item.ContextCommands = ContextCommands.Empty();
                item.PropertyChanged -= ItemOnPropertyChanged;
            }

            UpdateItemsOrder();
        }

        protected virtual void ClearAll()
        {
            foreach (var item in Collection)
            {
                item.ContextCommands = ContextCommands.Empty();
                item.PropertyChanged -= ItemOnPropertyChanged;
            }
            Collection.Clear();
        }

        public void Remove(TSingleItem item)
        {
            if (IsClearingItems) return;
            Collection.Remove(item);
            item.PropertyChanged -= ItemOnPropertyChanged;
        }

        protected virtual void UpdateItemsOrder()
        {
            var items = Collection.OrderBy(i => i.Order).ToImmutableArray();
            for (var i = 0; i < items.Length; i++)
            {
                items[i].Order = i;
            }
        }
    }
}