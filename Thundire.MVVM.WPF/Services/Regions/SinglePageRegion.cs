using System;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;

namespace Thundire.MVVM.WPF.Services.Regions
{
    internal class SinglePageRegion : IRegion, IViewRegion
    {
        private readonly ITemplatesCache _register;

        public SinglePageRegion(ITemplatesCache register)
        {
            _register = register;
        }

        public IRegionView RegionView { get; set; }

        public void Change(object content, string presenterKey = null)
        {
            if (RegionView.CurrentData is { } currentView)
            {
                if (content.GetType() == currentView.Template.DataType as Type)
                {
                    RegionView.ChangeContent(content);
                    return;
                }
            }

            var template = _register.GetTemplate(content, presenterKey);
            if (content is null || template is null)
            {
                throw new InvalidOperationException("Can't find presenter or content is null")
                {
                    Data = { ["content"] = content, ["template"] = template }
                };
            }
            RegionView.Change(new(content, template));
        }

        public void Close()
        {
            RegionView.Close();
        }

        public void Open()
        {
            if (RegionView.CurrentData is null)
                throw new InvalidOperationException("Region View don't has any View to show, first call method Change");
            RegionView.Show();
        }
    }
}