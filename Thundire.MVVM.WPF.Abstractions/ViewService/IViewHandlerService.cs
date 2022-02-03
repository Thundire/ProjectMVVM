namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewHandlerService
    {
        int CachedLength { get; }

        IViewOpener Search(string mark);
        IViewCloser Search(object key);
    }
}