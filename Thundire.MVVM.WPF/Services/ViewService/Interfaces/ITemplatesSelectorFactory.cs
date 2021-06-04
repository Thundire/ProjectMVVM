namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface ITemplatesSelectorFactory
    {
        TemplateSelector CreateSelector(string group);
    }
}