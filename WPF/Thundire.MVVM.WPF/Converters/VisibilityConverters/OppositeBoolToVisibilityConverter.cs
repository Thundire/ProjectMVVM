using System;
using System.Globalization;
using System.Windows;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.VisibilityConverters
{
    public class OppositeBoolToVisibilityConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool result)
                return !result ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }
    }
}
