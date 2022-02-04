namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IRegionView
    {
        PresenterData? CurrentData { get; }
        IViewRegion Region { get; set; }
        bool IsOpen { get; set; }
        bool IsContentVisible { get; set; }

        void Show();
        void Close();
        void Change(PresenterData view);
        void ChangeContent(object content);
        void ClearView();
        void Collapse();
    }
}