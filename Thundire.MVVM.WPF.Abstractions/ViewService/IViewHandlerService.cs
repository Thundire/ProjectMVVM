namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewHandlerService
    {
        IViewOpener Search(string mark);
        IViewCloser Search(object connector);
    }
}