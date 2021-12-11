using System;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public class CloseViewEventArgs
    {
        public EventArgs? InnerArgs { get; init; }

        public CloseViewEventArgs(EventArgs? originalArgs = null) => InnerArgs = originalArgs;

        public bool DialogResult { get; init; }
    }
}