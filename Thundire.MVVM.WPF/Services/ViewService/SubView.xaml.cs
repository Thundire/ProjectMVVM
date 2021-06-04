using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    public partial class SubView : UserControl
    {
        public SubView() => InitializeComponent();

        private static readonly DataTemplate DefaultTemplate = new() { VisualTree = new(typeof(ContentPresenter)) };
        
        public static readonly DependencyProperty SubViewSelectorProperty = DependencyProperty.Register(
            nameof(SubViewSelector),
            typeof(TemplateSelector),
            typeof(SubView),
            new(default(TemplateSelector), SelectorChanged)
        );

        public TemplateSelector SubViewSelector
        {
            get => (TemplateSelector)GetValue(SubViewSelectorProperty);
            set => SetValue(SubViewSelectorProperty, value);
        }

        private static void SelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not SubView subView) return;
            if (e.OldValue is TemplateSelector prev)
            {
                prev.PropertyChanged -= subView.SelectorInnerPropertyChanged;
            }

            if (e.NewValue is TemplateSelector next)
            {
                next.PropertyChanged += subView.SelectorInnerPropertyChanged;
                subView.UpdateState(next);
            }
        }

        private void UpdateState(TemplateSelector selector)
        {
            ChangeContent(selector.Content, selector.Template);
            Visibility = selector.IsShowView
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void SelectorInnerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is not TemplateSelector selector) return;
            switch (e.PropertyName)
            {
                case nameof(TemplateSelector.Content):
                    ChangeContent(selector.Content, selector.Template);
                    break;
                case nameof(TemplateSelector.Template):
                    ContentTemplate = selector.Template;
                    break;
                case nameof(TemplateSelector.IsShowView):
                    Visibility = selector.IsShowView
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;
            }
        }

        private void ChangeContent(object content, DataTemplate contentTemplate)
        {
            Content = content;
            ContentTemplate = contentTemplate ?? DefaultTemplate;
        }
    }
}
