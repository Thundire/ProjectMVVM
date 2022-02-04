using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using Thundire.MVVM.WPF.Converters.Base;

namespace Thundire.MVVM.WPF.Converters.ValueConverters
{
    [ValueConversion(typeof(bool),typeof(Brush))]
    [MarkupExtensionReturnType(typeof(BoolToBrushConverter))]
    public class BoolToBrushConverter : ConverterBase
    {
        [ConstructorArgument("successBrush")]
        public Brush? SuccessBrush { get; set; }

        [ConstructorArgument("failureBrush")]
        public Brush? FailureBrush { get; set; }

        [ConstructorArgument("wrongConversionBrush")]
        public Brush? WrongConversionBrush { get; set; }
        
        public BoolToBrushConverter()
        {

        }

        public BoolToBrushConverter(Brush successBrush, Brush failureBrush)
            : this(successBrush, failureBrush, new SolidColorBrush {Color = Colors.Transparent})
        {

        }

        public BoolToBrushConverter(Brush successBrush, Brush failureBrush, Brush wrongConversionBrush)
        {
            SuccessBrush = successBrush;
            FailureBrush = failureBrush;
            WrongConversionBrush = wrongConversionBrush;
        }
        
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value switch
                        {
                            true  => SuccessBrush,
                            false => FailureBrush,
                            _     => WrongConversionBrush
                        };
            return brush ?? DependencyProperty.UnsetValue;
        }
    }
}