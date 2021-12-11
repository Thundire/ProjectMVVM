namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IRegion
    {
        void Change(object content, string presenterKey = null);
        void Close();
        void Open();
    }
}