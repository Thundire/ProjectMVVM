using System.Windows;
using System.Windows.Controls;
using Thundire.MVVM.WPF.Abstractions.Regions;

// ReSharper disable InconsistentNaming

namespace Thundire.MVVM.WPF.Controls
{
    [TemplatePart(Name = Part_CloseBtn, Type = typeof(Button))]
    [TemplatePart(Name = Part_SwitchVisibilityStateBtn, Type = typeof(Button))]
    [TemplatePart(Name = Part_Content, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = Part_ContentPanel, Type = typeof(UIElement))]
    public class RegionView : Control, IRegionView
    {
        private const string Part_CloseBtn = nameof(Part_CloseBtn);
        private const string Part_SwitchVisibilityStateBtn = nameof(Part_SwitchVisibilityStateBtn);
        private const string Part_Content = nameof(Part_Content);
        private const string Part_ContentPanel = nameof(Part_ContentPanel);

        private Button _closeButton;
        private Button _switchVisibilityStateButton;
        private UIElement _contentPanel;
        private ContentPresenter _contentPresenter;

        private PresenterData _currentData;

        static RegionView() => DefaultStyleKeyProperty.OverrideMetadata(typeof(RegionView), new FrameworkPropertyMetadata(typeof(RegionView)));

        public static readonly DependencyProperty IsPanelVisibleProperty = DependencyProperty.Register(
            nameof(IsPanelVisible),
            typeof(bool),
            typeof(RegionView),
            new(default(bool)));

        public static readonly DependencyProperty RegionProperty = DependencyProperty.Register(
            nameof(Region),
            typeof(IViewRegion),
            typeof(RegionView),
            new FrameworkPropertyMetadata(default(IViewRegion))
            {
                PropertyChangedCallback = RegionChanged
            });
        
        public bool IsPanelVisible { get => (bool)GetValue(IsPanelVisibleProperty); set => SetValue(IsPanelVisibleProperty, value); }
        public IViewRegion Region { get => (IViewRegion)GetValue(RegionProperty); set => SetValue(RegionProperty, value); }
        
        private ContentPresenter ContentPresenter
        {
            get => _contentPresenter;
            set
            {
                _contentPresenter = value;
                if (value is null) return;
                value.Content = CurrentData?.Content;
                value.ContentTemplate = CurrentData?.Template;
            }
        }

        private UIElement ContentPanel
        {
            get => _contentPanel;
            set
            {
                _contentPanel = value;
                if (value.Visibility is Visibility.Visible) IsPanelVisible = true;
            }
        }

        public PresenterData CurrentData
        {
            get => _currentData;
            private set
            {
                _currentData = value;
                if (ContentPresenter is { } presenter)
                {
                    presenter.Content = value?.Content;
                    presenter.ContentTemplate = value?.Template;
                }
            }
        }

        public void Show()
        {
            if (Visibility is Visibility.Collapsed or Visibility.Hidden) Visibility = Visibility.Visible;
        }

        public void Close()
        {
            if (Visibility is Visibility.Visible or Visibility.Hidden) Visibility = Visibility.Collapsed;
        }

        public void Change(PresenterData data)
        {
            if (ContentPresenter is not null)
            {
                ContentPresenter.ContentTemplate = null;
            }
            CurrentData = data;
        }

        public void ChangeContent(object content)
        {
            CurrentData.Content = content;
            if (ContentPresenter is not null)
            {
                ContentPresenter.Content = content;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ConfigureCloseButton(GetTemplateChild(Part_CloseBtn) as Button);
            ConfigureSwitchVisibilityStateButton(GetTemplateChild(Part_SwitchVisibilityStateBtn) as Button);
            ContentPresenter = GetTemplateChild(Part_Content) as ContentPresenter;
            ContentPanel = GetTemplateChild(Part_ContentPanel) as UIElement;
        }

        private void ConfigureCloseButton(Button value)
        {
            if (_closeButton is { } btn) btn.Click -= CloseButtonClick;
            _closeButton = value;
            if (value is { }) value.Click += CloseButtonClick;
        }

        private void ConfigureSwitchVisibilityStateButton(Button value)
        {
            if (_switchVisibilityStateButton is { } btn) btn.Click -= SwitchVisibilityState;
            _switchVisibilityStateButton = value;
            if (value is { }) value.Click += SwitchVisibilityState;
        }

        private static void RegionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is IViewRegion old)
            {
                old.RegionView = null;
            }

            if (e.NewValue is IViewRegion newOne && d is IRegionView view)
            {
                newOne.RegionView = view;
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Region.Close();
        }

        private void SwitchVisibilityState(object sender, RoutedEventArgs e)
        {
            if (IsPanelVisible)
            {
                ContentPanel.Visibility = Visibility.Collapsed;
                IsPanelVisible = false;
                return;
            }
            ContentPanel.Visibility = Visibility.Visible;
            IsPanelVisible = true;
        }
    }
}