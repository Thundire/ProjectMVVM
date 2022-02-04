using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Thundire.MVVM.Core.Observable.ContextCollections;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.WPF.Observable.ContextCollections
{
    public class GroupItemsContextWithCollectionView<TGroupItem, TSingleItem> : GroupedItemsContext<TGroupItem, TSingleItem>
        where TSingleItem : SingleItemVM
        where TGroupItem : GroupItemVM
    {
        public GroupItemsContextWithCollectionView(GroupItemFactory<TGroupItem> groupFactory, IReadOnlyList<ItemVM>? items = null) : base(groupFactory, items)
        {
            var collectionSource = new CollectionViewSource
            {
                Source = Collection,
                IsLiveSortingRequested = true,
                SortDescriptions = { new SortDescription(nameof(ItemVM.Order), ListSortDirection.Ascending) },
            };
            CollectionView = collectionSource.View;
        }

        public ICollectionView CollectionView { get; }
    }
}