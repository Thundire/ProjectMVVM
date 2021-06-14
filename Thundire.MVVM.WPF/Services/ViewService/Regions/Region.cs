using System;
using System.Collections.Generic;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Regions.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Regions.Models;

namespace Thundire.MVVM.WPF.Services.ViewService.Regions
{
    internal class Region : IRegion, IViewRegion
    {
        private readonly ITemplatesCache _register;
        private readonly Stack<PresenterData> _dataCache = new();

        public Region(ITemplatesCache register) => _register = register;

        public IRegionView RegionView { get; set; }

        public void Open()
        {
            if (_dataCache.TryPop(out var view)) RegionView.Change(view);
            RegionView.Show();
        }

        public void Close()
        {
            if (!_dataCache.TryPop(out var view))
            {
                RegionView.Close();
                return;
            }
            RegionView.Change(view);
        }

        public void Change(object content, string presenterKey = null)
        {
            if (RegionView.CurrentData is { } currentView)
            {
                _dataCache.Push(currentView);
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
            _dataCache.Push(new(content, template));
        }
    }
}