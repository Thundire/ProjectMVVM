using System;
using System.Collections.Generic;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;

namespace Thundire.MVVM.WPF.Regions
{
    internal class StackViewsRegion : RegionBase
    {
        private readonly List<PresenterData> _dataCache = new();
        private int _index = -1;
        public StackViewsRegion(ITemplatesCache register) : base(register) {}
        
        protected override void ChangeRegion(IRegionView regionView, object content, string? presenterKey = null)
        {
            if (regionView.CurrentData is { } currentView)
            {
                if (currentView.GetHashCode() == content.GetHashCode()) return;
                if (content.GetType() == (currentView.Template.DataType as Type))
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

            var presenter = new PresenterData(content, template);
            regionView.Change(presenter);
            _dataCache.Add(regionView.CurrentData);
            _index++;
        }

        protected override void CloseRegion(IRegionView regionView)
        {
            if (_index > 0)
            {
                _dataCache.Remove(_dataCache[_index]);
                regionView.Change(_dataCache[--_index]);
                return;
            }
            _dataCache.Clear();
            _index--;
            regionView.Close();
            regionView.Change(null);
        }
    }
}