using System.Collections.Generic;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.Core.Observable.ContextCollections
{
    public delegate TGroupItem GroupItemFactory<out TGroupItem>(IReadOnlyCollection<SingleItemVM> items) where TGroupItem : GroupItemVM;
}