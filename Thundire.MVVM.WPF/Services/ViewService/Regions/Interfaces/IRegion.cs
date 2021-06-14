namespace Thundire.MVVM.WPF.Services.ViewService.Regions
{
    public interface IRegion
    {
        void Change(object content, string presenterKey = null);
        void Close();
        void Open();
    }
}