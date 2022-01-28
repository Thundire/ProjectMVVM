using System;

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
    }
}