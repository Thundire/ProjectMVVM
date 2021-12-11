namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IViewRegion
    {
        IRegionView RegionView { get; set; }

        void Close();
    }
}