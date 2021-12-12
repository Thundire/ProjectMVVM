namespace Thundire.MVVM.WPF.Abstractions.PagesNavigator
{
    public interface INavigator
    {
        string? CurrentPage { get; }

        void NavigateTo(string pageName, object data);
        void UsePagesGroup(string group);
    }
}