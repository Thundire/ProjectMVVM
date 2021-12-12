using System;
using System.Globalization;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.ValueConverters
{
#nullable disable
    public class StringEmptyToNullConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value is string str and not "" && string.IsNullOrWhiteSpace(str) ? null : value;
    }
#nullable enable
}