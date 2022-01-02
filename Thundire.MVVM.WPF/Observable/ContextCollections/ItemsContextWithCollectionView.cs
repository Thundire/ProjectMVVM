using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Thundire.MVVM.Core.Observable.ContextCollections;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.WPF.Observable.ContextCollections
{
    public class ItemsContextWithCollectionView<TSingleItem> : ItemsContext<TSingleItem> where TSingleItem : SingleItemVM
    {
        public ItemsContextWithCollectionView(IReadOnlyList<ItemVM>? items = null) : base(items)
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