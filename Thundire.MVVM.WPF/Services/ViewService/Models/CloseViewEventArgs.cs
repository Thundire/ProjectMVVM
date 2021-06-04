using System;

namespace Thundire.MVVM.WPF.Services.ViewService.Models
{
    public class CloseViewEventArgs
    {
        public EventArgs InnerArgs { get; init; }

        public CloseViewEventArgs(EventArgs originalArgs = null) => InnerArgs = originalArgs;

        public bool DialogResult { get; init; }
    }
}