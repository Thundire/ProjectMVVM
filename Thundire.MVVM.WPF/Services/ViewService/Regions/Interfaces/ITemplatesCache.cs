using System.Windows;

namespace Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces
{
    public interface ITemplatesCache
    {
        DataTemplate GetTemplate(object content, string presenterKey = null);
    }
}