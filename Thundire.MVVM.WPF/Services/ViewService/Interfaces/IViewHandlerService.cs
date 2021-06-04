namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IViewHandlerService
    {
        IViewOpener Search(string mark);
        IViewCloser Search(object connector);
    }
}