using System;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewOpener
    {
        TView? Handle<TView>() where TView : class;
        IViewOpener WithOwner(object owner);
        IViewOpener OnClose(CloseViewEventHandler onClose);
        IViewOpener ViewBehaviorOnLoaded<TView>(Action<TView> onLoaded);
        IViewOpener DataContextBehaviorOnLoaded<TDataContext>(Action<TDataContext> onLoaded);
        IViewOpener SetDataContext(object dataContext);
        TView? Handle<TView>(object key) where TView : class;
    }
}