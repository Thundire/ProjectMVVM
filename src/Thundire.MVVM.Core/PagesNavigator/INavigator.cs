namespace Thundire.MVVM.Core.PagesNavigator
{
    public interface INavigator
    {
        string? CurrentPageKey { get; }
        object? CurrentDataContext { get; }

        void ChangeDataContextOfCurrentPage(object dataContext);
        void NavigateTo(string pageName, object? dataContext);
        void UsePagesGroup(string group);
    }
}