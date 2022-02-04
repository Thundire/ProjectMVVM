using System;

using Thundire.MVVM.WPF.Abstractions.Regions;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;

namespace Thundire.MVVM.WPF.Regions
{
    public abstract class RegionBase : IRegion
    {
        protected readonly ITemplatesCache TemplatesRegister;

        protected RegionBase(ITemplatesCache register) => TemplatesRegister = register;

        public IRegionView? RegionView { get; set; }

        public void Change(object content, string? presenterKey = null)
        {
            if (RegionView is null) throw RegionViewNullException;

            ChangeRegion(RegionView, content, presenterKey);
        }

        public void Close()
        {
            if (RegionView is null) throw RegionViewNullException;

            CloseRegion(RegionView);
        }

        public void Open()
        {
            if (RegionView is null) throw RegionViewNullException;

            OpenRegion(RegionView);
        }

        public void Collapse()
        {
            if (RegionView is null) throw RegionViewNullException;

            RegionView.Collapse();
        }

        private static Exception RegionViewNullException => new InvalidOperationException("Not registered RegionView");

        protected abstract void ChangeRegion(IRegionView regionView, object content, string? presenterKey = null);

        protected virtual void CloseRegion(IRegionView regionView)
        {
            regionView.Close();
        }

        protected virtual void OpenRegion(IRegionView regionView)
        {
            if (regionView.CurrentData is null)
                throw new InvalidOperationException("Region View don't has any View to show, first call method Change");
            regionView.Show();
        }

        protected PresenterData CreatePresenterData(object content, string presenterKey)
        {
            var template = TemplatesRegister.GetTemplate(content, presenterKey);
            if (template is null)
            {
                throw new InvalidOperationException("Can't find presenter for content")
                {
                    Data = { [nameof(content)] = content, [nameof(presenterKey)] = presenterKey }
                };
            }

            var presenter = new PresenterData(content, template) { PresenterKey = presenterKey };
            return presenter;
        }

        protected PresenterData CreatePresenterData(object content)
        {
            var template = TemplatesRegister.GetTemplate(content);
            if (template is null)
            {
                throw new InvalidOperationException("Can't find presenter for content")
                {
                    Data = { ["content"] = content }
                };
            }

            var presenter = new PresenterData(content, template);
            return presenter;
        }
    }
}