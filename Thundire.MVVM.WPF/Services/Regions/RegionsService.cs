using System.Collections.Generic;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;

namespace Thundire.MVVM.WPF.Services.Regions
{
    public class RegionsService
    {
        private static Dictionary<string, IRegion> Regions { get; } = new();

        private readonly ITemplatesCache _templatesCache;

        public RegionsService(ITemplatesCache templatesCache) => _templatesCache = templatesCache;

        public IRegion GetRegion(string key) => Regions.TryGetValue(key, out var region) ? region : null;

        public IRegion CreateSinglePageRegion(string key)
        {
            var region = new SinglePageRegion(_templatesCache);
            return Regions.TryAdd(key, region)
                ? region 
                : Regions[key];
        }

        public IRegion CreateStackViewsRegion(string key)
        {
            var region = new StackViewsRegion(_templatesCache);
            return Regions.TryAdd(key, region)
                ? region
                : Regions[key];
        }
    }
}