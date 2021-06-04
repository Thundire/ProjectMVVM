using System;
using System.Globalization;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.ValueConverters
{
    public class IntToNullableIntConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value is null ? default(int) : System.Convert.ChangeType(value, typeof(int));

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value is int number ? new int?(number) : null;
    }
}