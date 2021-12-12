namespace Thundire.MVVM.WPF.ViewService
{
    public interface IViewsCache
    {
        View Get(string mark);
        bool TryGetView(object owner, out View view);
    }
}