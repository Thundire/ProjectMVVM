using System.ComponentModel;
using System.Windows.Data;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.WPF.Observable.ContextCollections
{
    public class GroupItemVMWithCollectionView : GroupItemVM
    {
        public GroupItemVMWithCollectionView()
        {
            var collectionSource = new CollectionViewSource
            {
                Source = Inner,
                IsLiveSortingRequested = true,
                SortDescriptions = { new SortDescription(nameof(ItemVM.Order), ListSortDirection.Ascending) },
            };
            InnerView = collectionSource.View;
        }

        public ICollectionView InnerView { get; }
    }
}