namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewRegisterCache
    {
        ViewDescriptor GetView(string mark);
        bool IsMarkNotRegistered(string mark);
    }
}