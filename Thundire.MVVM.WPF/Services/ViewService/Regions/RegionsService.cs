using System.Collections.Generic;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces;

namespace Thundire.MVVM.WPF.Services.ViewService.Regions
{
    public class RegionsService
    {
        private static Dictionary<string, Region> Regions { get; } = new();

        private readonly ITemplatesCache _templatesCache;

        public RegionsService(ITemplatesCache templatesCache) => _templatesCache = templatesCache;

        public static IRegion GetRegion(string key) => Regions.TryGetValue(key, out var region) ? region : null;

        public IRegion CreateRegion(string key)
        {
            var region = new Region(_templatesCache);
            return Regions.TryAdd(key, region)
                ? region 
                : Regions[key];
        }
    }
}