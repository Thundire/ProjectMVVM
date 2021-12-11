using System;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IViewOpener
    {
        TView Handle<TView>() where TView : class;
        IViewOpener WithOwner(object owner);
        IViewOpener OnClose(CloseViewEventHandler onClose);
        IViewOpener OnLoaded(Action<object> onLoaded);
    }
}