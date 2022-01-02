using System.Windows;

namespace Thundire.MVVM.WPF.Abstractions.TemplatesCache
{
    public interface ITemplatesCache
    {
        DataTemplate? GetTemplate(object content, string? presenterKey = null);
    }
}