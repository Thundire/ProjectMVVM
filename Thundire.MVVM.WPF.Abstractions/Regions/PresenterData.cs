using System.Windows;

namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public class PresenterData
    {
        public PresenterData(object content, DataTemplate template)
        {
            Content = content;
            Template = template;
        }

        public object Content { get; set; }
        public DataTemplate Template { get; set; }
    }
}