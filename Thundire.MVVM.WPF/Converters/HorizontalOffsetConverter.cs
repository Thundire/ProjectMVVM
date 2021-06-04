using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Thundire.MVVM.WPF.Converters
{
    public class HorizontalOffsetConverter : Freezable, IValueConverter
    {
        public static readonly DependencyProperty ParentActualWidthProperty = DependencyProperty.Register(
            nameof(ParentActualWidth), typeof(double), typeof(HorizontalOffsetConverter), new PropertyMetadata(default(double)));
        public double ParentActualWidth
        {
            get => (double) GetValue(ParentActualWidthProperty);
            set => SetValue(ParentActualWidthProperty, value);
        }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double width) return default;
            return ParentActualWidth - width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        protected override Freezable CreateInstanceCore() => new HorizontalOffsetConverter(){ParentActualWidth = ParentActualWidth};
    }
}