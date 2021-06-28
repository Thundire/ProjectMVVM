namespace Thundire.MVVM.WPF.Services.Navigator.Interfaces
{
    public interface INavigator
    {
        string CurrentPage { get; }

        void NavigateTo(string pageName, object data);
        void UsePagesGroup(string group);
    }
}