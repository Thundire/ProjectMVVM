namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IRegion : IViewRegion
    {
        void Change(object content, string? presenterKey = null);
        void Open();
    }
}