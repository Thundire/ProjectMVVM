namespace Thundire.MVVM.WPF.Abstractions.Regions
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