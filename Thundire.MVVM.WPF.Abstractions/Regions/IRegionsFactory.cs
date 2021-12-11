namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IRegionsFactory
    {
        IRegion CreateSinglePageRegion(string key);
        IRegion CreateStackViewsRegion(string key);
        IRegion GetRegion(string key);
    }
}