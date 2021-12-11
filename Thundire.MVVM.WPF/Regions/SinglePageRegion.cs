using System;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;

namespace Thundire.MVVM.WPF.Regions
{
    internal class SinglePageRegion : RegionBase
    {
        public SinglePageRegion(ITemplatesCache register) : base(register){}
        
        protected override void ChangeRegion(IRegionView regionView, object content, string? presenterKey = null)
        {
            if (regionView.CurrentData is { } currentView)
            {
                if (content.GetType() == currentView.Template.DataType as Type)
                {
                    regionView.ChangeContent(content);
                    return;
                }
            }

            var template = TemplatesRegister.GetTemplate(content, presenterKey);
            if (content is null || template is null)
            {
                throw new InvalidOperationException("Can't find presenter or content is null")
                {
                    Data = { ["content"] = content, ["template"] = template }
                };
            }
            regionView.Change(new(content, template));
        }
    }
}