using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Thundire.MVVM.WPF.Behaviors
{
    public class SetNavigator : Behavior<Frame>
    {
        public static readonly DependencyProperty NavigatorProperty = DependencyProperty.Register(
            nameof(Navigator),
            typeof(INavigationHandler),
            typeof(SetNavigator),
            new(default(INavigationHandler), NavigatorChanged));

        public INavigationHandler? Navigator { get => (INavigationHandler)GetValue(NavigatorProperty); set => SetValue(NavigatorProperty, value); }

        private NavigationService? _navigator;

        protected override void OnAttached()
        {
            _navigator = AssociatedObject.NavigationService;
        }

        protected override void OnDetaching()
        {
            if(Navigator is not null) Navigator.NavigationService = null;
            _navigator = null;
        }

        private static void NavigatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not SetNavigator behavior || e.NewValue is not INavigationHandler next) return;
            if (e.OldValue is INavigationHandler prev) prev.NavigationService = null;
            next.NavigationService = behavior._navigator;
        }
    }
}
