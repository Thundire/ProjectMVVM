﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
            if (value is null) return "";
            foreach (var one in Enum.GetValues(parameter as Type ?? throw new InvalidOperationException()))
            {
                if (value.Equals(one))
                    return (one as Enum).GetDescription();
            }
            return "";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            return Enum.GetValues(parameter as Type ?? throw new InvalidOperationException())
                .Cast<object>()
                .FirstOrDefault(one => value.ToString() == (one as Enum).GetDescription());
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
            var attribute = (field ?? throw new InvalidOperationException()).GetCustomAttribute<DescriptionAttribute>();
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
