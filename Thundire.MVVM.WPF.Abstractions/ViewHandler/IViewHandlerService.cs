namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IViewHandlerService
    {
        IViewOpener Search(string mark);
        IViewCloser Search(object connector);
    }
}