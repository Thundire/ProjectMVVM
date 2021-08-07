namespace Thundire.MVVM.WPF.Services.Regions.Interfaces
{
    public interface IRegionsFactory
    {
        IRegion CreateSinglePageRegion(string key);
        IRegion CreateStackViewsRegion(string key);
        IRegion GetRegion(string key);
    }
}