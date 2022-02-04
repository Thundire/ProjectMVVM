using System;
using System.Globalization;
using System.Windows;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.VisibilityConverters
{
    public class NullableVisibilityConverter: ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value is {} ? Visibility.Visible : Visibility.Collapsed;
    }
}