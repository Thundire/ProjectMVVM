using System;
using System.Globalization;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.ValueConverters
{
    public class OppositeBoolConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is false;
    }
}