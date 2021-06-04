using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Thundire.MVVM.WPF.Observable.Base
{
    public class DispatcherNotifyBase : NotifyBase, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handlers = PropertyChanged;
            if (handlers is null) return;
            var invocationList = handlers.GetInvocationList();

            var arg = new PropertyChangedEventArgs(propertyName);
            foreach (var action in invocationList)
                switch (action.Target)
                {
                    case DispatcherObject dispatcherObject:
                        if (!dispatcherObject.CheckAccess()) dispatcherObject.VerifyAccess();
                        dispatcherObject.Dispatcher.BeginInvoke(action, this, arg);
                        break;
                    default:
                        action.DynamicInvoke(this, arg);
                        break;
                }
        }
    }
}