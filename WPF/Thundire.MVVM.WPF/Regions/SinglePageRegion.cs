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
            PresenterData? presenter;
            // regionView don't contains any presenter, so set new
            if (regionView.CurrentData is not { } currentView)
            {
                presenter = presenterKey is null
                    ? CreatePresenterData(content)
                    : CreatePresenterData(content, presenterKey);
                regionView.Change(presenter);
                return;
            }

            if (presenterKey is not null)
            {
                if(currentView.PresenterKey == presenterKey) return;

                presenter = CreatePresenterData(content, presenterKey);
                regionView.Change(presenter);
            }

            // if template for content is valuable, change only content
            if (content.GetType() == currentView.Template.DataType as Type)
            {
                regionView.ChangeContent(content);
                return;
            }

            presenter = CreatePresenterData(content);
            regionView.Change(presenter);
        }
    }
}