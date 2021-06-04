using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Thundire.MVVM.WPF.Converters.ValueConverters
{
    public class ListToStringConverter : Freezable, IValueConverter
    {
        public static readonly DependencyProperty EnumerationProperty = DependencyProperty.Register(
            nameof(Enumeration),
            typeof(List<string>),
            typeof(ListToStringConverter),
            new PropertyMetadata(default(List<string>)));

        public List<string> Enumeration
        {
            get => (List<string>)GetValue(EnumerationProperty);
            init => SetValue(EnumerationProperty, value);
        }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            Enumeration is {Count: > 0} ? string.Join(", ", Enumeration) : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string str) throw new ArgumentException("Converted value must be string");

            var array = str.Trim().TrimEnd(',').Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s=>s.Trim());
            Enumeration.Clear();
            array.ToList().ForEach(Enumeration.Add);
            return Enumeration;
        }
        
        protected override Freezable CreateInstanceCore() => new ListToStringConverter {Enumeration=Enumeration};
    }
}