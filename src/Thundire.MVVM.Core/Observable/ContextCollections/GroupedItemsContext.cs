using System;
using System.Collections.Generic;
using System.Linq;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.Core.Observable.ContextCollections
{
    public class GroupedItemsContext<TGroupItem, TSingleItem> : ItemsContext<TSingleItem> where TGroupItem : GroupItemVM where TSingleItem : SingleItemVM
    {
        private readonly GroupItemFactory<TGroupItem> _groupFactory;
        private Func<ContextCommands>? _groupContextCommands;

        public GroupedItemsContext(GroupItemFactory<TGroupItem> groupFactory, IReadOnlyList<ItemVM>? items = null) : base(items) => _groupFactory = groupFactory;

        public Func<ContextCommands>? GroupContextCommands
        {
            get => _groupContextCommands;
            set
            {
                _groupContextCommands = value;

                if (Collection.Count <= 0) return;
                var groups = Collection.OfType<TGroupItem>().ToList();

                if (value is not null)
                {
                    foreach (var item in groups)
                    {
                        item.ContextCommands = value.Invoke();
                    }
                    return;
                }

                foreach (var item in groups)
                {
                    item.ContextCommands = ContextCommands.Empty();
                }
            }
        }

        public void Ungroup()
        {
            var groups = Collection.OfType<GroupItemVM>().Where(i => i.HasSelectedItems).ToList();
            foreach (var item in groups)
            {
                if (item.IsAllItemsSelected)
                {
                    Collection.Remove(item);
                    item.ContextCommands = ContextCommands.Empty();
                }

                var selected = item.Where(i => i.IsSelected).ToList();
                foreach (var groupedItemVM in selected)
                {
                    item.RemoveItem(groupedItemVM);
                    groupedItemVM.Order = Collection.Count;
                    Collection.Add(groupedItemVM);
                }
                item.IsSelected = false;
            }
        }

        public void Regroup()
        {
            List<SingleItemVM>? temp = null;
            var selectedItems = Collection.Where(i => i.IsSelected).ToList();
            TGroupItem? groupToMerge = null;
            if (selectedItems.Count(i => i is TGroupItem) == 1 && selectedItems.SingleOrDefault(i => i is TGroupItem) is TGroupItem { IsAllItemsSelected: true } singleGroup)
            {
                groupToMerge = singleGroup;
                selectedItems.Remove(singleGroup);
            }

            foreach (var item in selectedItems)
            {
                temp ??= new List<SingleItemVM>();
                switch (item)
                {
                    case GroupItemVM group:
                    {
                        var selected = group.Where(i => i.IsSelected).ToList();

                        foreach (var groupedItemVM in selected)
                        {
                            group.RemoveItem(groupedItemVM);
                            temp.Add(groupedItemVM);
                        }
                        if (!group.Any()) Collection.Remove(item);
                        group.IsSelected = false;
                        break;
                    }
                    case SingleItemVM single:
                        temp.Add(single);
                        Collection.Remove(item);
                        break;
                    default: continue;
                }
            }

            if (temp is null) return;

            if (groupToMerge is not null)
            {
                groupToMerge.AppendItems(temp);
                return;
            }

            groupToMerge = _groupFactory.Invoke(temp);
            if (_groupContextCommands is not null) { groupToMerge.ContextCommands = _groupContextCommands.Invoke(); }
            groupToMerge.IsSelected = true;

            UpdateItemsOrder();

            groupToMerge.Order = Collection.Count;
            Collection.Add(groupToMerge);
        }

        protected override IReadOnlyCollection<TSingleItem> GetExistedItems()
        {
            List<TSingleItem> items = new();
            foreach (var item in Collection)
            {
                switch (item)
                {
                    case TGroupItem group:
                        var singleItems = group.OfType<TSingleItem>();
                        items.AddRange(singleItems);
                        break;
                    case TSingleItem single:
                        items.Add(single);
                        break;
                }
            }

            return items;
        }

        protected override void RemoveSelected()
        {
            foreach (var item in Collection.Where(i => i.IsSelected).ToList())
            {
                switch (item)
                {
                    case GroupItemVM { IsAllItemsSelected: true } group:
                    {
                        Collection.Remove(group);
                        group.ContextCommands = ContextCommands.Empty();
                        break;
                    }
                    case GroupItemVM { IsAllItemsSelected: false } group:
                    {
                        var selected = group.Where(i => i.IsSelected).ToList();

                        foreach (var groupedItemVM in selected)
                        {
                            group.RemoveItem(groupedItemVM);
                            groupedItemVM.ContextCommands = ContextCommands.Empty();
                            groupedItemVM.PropertyChanged -= ItemOnPropertyChanged;
                        }

                        group.IsSelected = false;

                        break;
                    }
                    default:
                        Collection.Remove(item);
                        item.ContextCommands = ContextCommands.Empty();
                        break;
                }
            }

            UpdateItemsOrder();
        }

        public void Remove(TGroupItem group)
        {
            if (IsClearingItems) return;

            Collection.Remove(group);
            group.PropertyChanged -= ItemOnPropertyChanged;
            foreach (var item in group)
            {
                item.PropertyChanged -= ItemOnPropertyChanged;
            }
        }

        public void Ungroup(TGroupItem group)
        {
            foreach (var groupedItem in group.ToList())
            {
                group.RemoveItem(groupedItem);
                groupedItem.Order = Collection.Count;
                Collection.Add(groupedItem);
                groupedItem.Group = null;
            }

            Collection.Remove(group);
            group.ContextCommands = ContextCommands.Empty();
        }
    }
}