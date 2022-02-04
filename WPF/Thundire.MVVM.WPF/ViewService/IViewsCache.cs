using System.Diagnostics.CodeAnalysis;

namespace Thundire.MVVM.WPF.ViewService
{
    public interface IViewsCache
    {
        View Get(string mark);
        View Get(string mark, object key);
        bool TryGetView(object owner,[NotNullWhen(true)] out View? view);
    }
}