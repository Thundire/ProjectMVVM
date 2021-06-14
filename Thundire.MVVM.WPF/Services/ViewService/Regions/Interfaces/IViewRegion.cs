namespace Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces
{
    public interface IViewRegion
    {
        IRegionView RegionView { get; set; }

        void Close();
    }
}