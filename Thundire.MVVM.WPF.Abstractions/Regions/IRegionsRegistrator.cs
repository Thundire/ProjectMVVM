namespace Thundire.MVVM.WPF.Abstractions.Regions
{
    public interface IRegionsRegistrator
    {
        IRegionsRegistrator RegisterSinglePageRegion(string key);
        IRegionsRegistrator RegisterStackViewsRegion(string key);
    }
}