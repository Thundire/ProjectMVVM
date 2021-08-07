namespace Thundire.MVVM.WPF.Services.Regions.Interfaces
{
    public interface IRegionsRegistrator
    {
        IRegionsRegistrator RegisterSinglePageRegion(string key);
        IRegionsRegistrator RegisterStackViewsRegion(string key);
    }
}