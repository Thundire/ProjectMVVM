using System;
using System.Globalization;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.ValueConverters
{
    public class TimeZoneConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string zoneId)
                return TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeZoneInfo zone)
                return zone.Id;
            return null;
        }
    }
}
