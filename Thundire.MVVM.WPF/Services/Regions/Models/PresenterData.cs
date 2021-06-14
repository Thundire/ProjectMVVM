using System.Windows;

namespace Thundire.MVVM.WPF.Services.Regions.Models
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