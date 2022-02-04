namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IViewRegion
    {
        /// <summary>
        /// Region view that bind to region
        /// </summary>
        IRegionView? RegionView { get; set; }

        void Close();
    }
}