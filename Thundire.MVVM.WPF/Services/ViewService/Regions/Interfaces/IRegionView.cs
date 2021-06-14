using Thundire.MVVM.WPF.Services.ViewService.Regions.Models;

namespace Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces
{
    public interface IRegionView
    {
        PresenterData CurrentData { get; }

        void Show();
        void Close();
        void Change(PresenterData view);
        void ChangeContent(object content);
    }
}