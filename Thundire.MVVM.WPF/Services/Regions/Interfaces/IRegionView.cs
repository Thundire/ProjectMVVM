using Thundire.MVVM.WPF.Services.Regions.Models;

namespace Thundire.MVVM.WPF.Services.Regions.Interfaces
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