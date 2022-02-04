using System;
using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewOpener
    {
        IViewOpener WithOwner(object owner);
        IViewOpener SetDataContext(object dataContext);

        IViewOpener OnClose(CloseViewEventHandler onClose);

        IViewOpener ViewBehaviorOnLoaded<TView>(Action<TView> onLoaded);
        IViewOpener DataContextBehaviorOnLoaded<TDataContext>(Action<TDataContext> onLoaded);
        IViewOpener NavigateOnLoaded(string pageKey);

        TView? Handle<TView>() where TView : class;
        TView? Handle<TView>(object key) where TView : class;
        IViewOpener ViewBehaviorOnLoaded<TView>(Func<TView, Task> onLoaded);
        IViewOpener DataContextBehaviorOnLoaded<TDataContext>(Func<TDataContext, Task> onLoaded);
    }
}