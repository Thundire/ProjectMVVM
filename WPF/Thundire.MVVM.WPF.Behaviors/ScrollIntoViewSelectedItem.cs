using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Thundire.MVVM.WPF.Behaviors
{
    public class ScrollIntoViewSelectedItemBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += ScrollToSelected;
            AssociatedObject.Loaded += ScrollToSelected;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= ScrollToSelected;
            AssociatedObject.Loaded -= ScrollToSelected;
        }
        
        private static void ScrollToSelected(object sender, RoutedEventArgs e)
        {
            var box = sender as ListBox;
            box?.ScrollIntoView(box.SelectedItem);
        }
    }
}