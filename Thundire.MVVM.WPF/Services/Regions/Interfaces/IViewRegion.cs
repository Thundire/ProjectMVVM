namespace Thundire.MVVM.WPF.Services.Regions.Interfaces
{
    public interface IViewRegion
    {
        IRegionView RegionView { get; set; }

        void Close();
    }
}