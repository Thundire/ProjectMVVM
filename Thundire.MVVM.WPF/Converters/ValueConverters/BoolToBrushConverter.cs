using System;
using System.Globalization;
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
        public Brush SuccessBrush { get; set; }

        [ConstructorArgument("failureBrush")]
        public Brush FailureBrush { get; set; }

        [ConstructorArgument("wrongConversionBrush")]
        public Brush WrongConversionBrush { get; set; }
        
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
            if (value is bool v) return v ? SuccessBrush : FailureBrush;
            return WrongConversionBrush;
        }
    }
}