using System;
using System.Windows.Markup;

namespace Thundire.MVVM.WPF.Converters.EnumToView
{
    /// <summary>
    /// Markup Extension that helps bind view to enum as collection of it's values
    ///
    /// SelectedIndex="2" SelectedValue="{Binding EnumProperty}"
    /// ItemsSource="{Binding Source={enumToView:EnumBindingSourceExtension EnumType}}"
    /// 
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        public Type EnumType { get; }

        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType is {IsEnum: true})
                EnumType = enumType; 
            else throw new ArgumentException("Enum Type must not be null and of type Enum");
        }
        
        public override object ProvideValue(IServiceProvider serviceProvider) => Enum.GetValues(EnumType);
    }
}