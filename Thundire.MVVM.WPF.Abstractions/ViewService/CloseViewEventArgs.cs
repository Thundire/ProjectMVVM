using System;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public class CloseViewEventArgs
    {
        public EventArgs? InnerArgs { get; init; }

        public CloseViewEventArgs(EventArgs? originalArgs = null, bool dialogResult = default)
        {
            InnerArgs = originalArgs;
            DialogResult = dialogResult;
        }

        public bool DialogResult { get; init; }
    }

    public sealed class CloseViewEventArgs<TResult> : CloseViewEventArgs
    {
        public CloseViewEventArgs(EventArgs? originalArgs = null, bool dialogResult = default, TResult? result = default)
            : base(originalArgs, dialogResult) =>
            Result = result;
        
        public TResult? Result { get; init; }
    }
}