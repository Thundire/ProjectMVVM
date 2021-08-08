namespace Thundire.MVVM.WPF.Observable.EditForm
{
    public class EditFormResultArgs<TModel> : EditFormResultArgs
    {
        public TModel Edited { get; init; }
    }
}