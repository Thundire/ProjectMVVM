using System;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IViewOpener
    {
        TView Handle<TView>() where TView : class;
        IViewOpener WithOwner(object owner);
        IViewOpener OnClose(CloseViewEventHandler onClose);
        IViewOpener OnLoaded(Action<object> onLoaded);
    }
}