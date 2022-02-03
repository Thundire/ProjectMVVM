namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public class ViewDescriptor
    {
        public ViewDescriptor(IView view, object? dataContext = null)
        {
            View = view;
            DataContext = dataContext;
        }

        public IView View { get; }
        public object? DataContext { get; }
        public bool HasDataContext => DataContext is not null;
    }
}