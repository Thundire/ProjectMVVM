using System;
using System.Collections.Generic;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;
using Thundire.MVVM.WPF.Services.Regions.Models;

namespace Thundire.MVVM.WPF.Services.Regions
{
    internal class StackViewsRegion : IRegion, IViewRegion
    {
        private readonly ITemplatesCache _register;
        private readonly Stack<PresenterData> _dataCache = new();

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
            if (!_dataCache.TryPop(out var view))
            {
                RegionView.Close();
                RegionView.Change(null);
                return;
            }
            RegionView.Change(view);
        }

        public void Change(object content, string presenterKey = null)
        {
            if (RegionView.CurrentData is { } currentView && content.GetType() == currentView.Template.DataType as Type)
            {
                RegionView.ChangeContent(content);
                return;
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
            _dataCache.Push(presenter);
            RegionView.Change(presenter);
        }
    }
}