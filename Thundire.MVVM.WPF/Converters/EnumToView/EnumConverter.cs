using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.EnumToView
{
    /// <summary>
    /// ItemsSource="{Binding Source={enumToView:EnumToItemsSource {x:Type EnumType}}}"
    /// SelectedValue="{Binding EnumProperty, Converter={StaticResource EnumConverter}, ConverterParameter={x:Type EnumType}}"
    /// </summary>
    public class EnumConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ReSharper disable once InvertIf
            if (parameter is Enum enumToParse)
            {
                foreach (Enum enumValue in Enum.GetValues(enumToParse.GetType()))
                {
                    if (Equals(value, enumValue))
                        return enumValue.GetDescription();
                }
            }

            return "";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not Enum enumToParse) return DependencyProperty.UnsetValue;
            var enumResult = Enum.GetValues(enumToParse.GetType())
                .Cast<Enum>()
                .FirstOrDefault(enumValue => value.ToString() == enumValue.GetDescription());
            return enumResult ?? DependencyProperty.UnsetValue;
        }
    }


    public static class EnumElementDescriptionReader
    {
        /// <summary> Casting an enumeration value into a human-readable format. </summary>
        /// <remarks> For correct operation, it is necessary to use the [Description ("Name")] attribute for each enumeration element. </remarks>
        /// 
        /// <param name="enumElement">Listing item</param>
        /// <returns>Item name</returns>
        public static string GetDescription(this Enum enumElement)
        {
            var field = enumElement.GetType().GetField(enumElement.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? enumElement.ToString();
        }
    }


    public class EnumToItemsSource : MarkupExtension
    {
        private readonly Type _type;

        public EnumToItemsSource(Type type) => _type = type;

        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _type.GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .Cast<DescriptionAttribute>())
                .Select(x => x.Description)
                .ToList();
    }
}
