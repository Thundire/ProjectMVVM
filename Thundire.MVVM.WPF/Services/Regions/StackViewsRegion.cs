using System;
using System.Collections.Generic;
using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;

namespace Thundire.MVVM.WPF.Services.Regions
{
    internal class StackViewsRegion : IRegion, IViewRegion
    {
        private readonly ITemplatesCache _register;
        private readonly List<PresenterData> _dataCache = new();
        private int _index = -1;
        public StackViewsRegion(ITemplatesCache register) => _register = register;

        public IRegionView RegionView { get; set; }

        public void Open()
        {
            if (RegionView.CurrentData is null)
                throw new InvalidOperationException("Region View don't has any View to show, first call method Change");
            RegionView.Show();
        }

        public void Close()
        {
            if (_index > 0)
            {
                _dataCache.Remove(_dataCache[_index]);
                RegionView.Change(_dataCache[--_index]);
                return;
            }
            _dataCache.Clear();
            _index--;
            RegionView.Close();
            RegionView.Change(null);
        }

        public void Change(object content, string presenterKey = null)
        {
            if (RegionView.CurrentData is { } currentView)
            {
                if(currentView.GetHashCode() == content.GetHashCode()) return;
                if (content.GetType() == (currentView.Template.DataType as Type))
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

            var presenter = new PresenterData(content, template);
            RegionView.Change(presenter);
            _dataCache.Add(RegionView.CurrentData);
            _index++;
        }
    }
}