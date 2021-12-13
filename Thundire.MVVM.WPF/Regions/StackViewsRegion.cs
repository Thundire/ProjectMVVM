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
        public StackViewsRegion(ITemplatesCache register) : base(register) { }

        // TODO: possible that we just switching from edit to preview and back
        // TODO: possible tree node call that expect return to previous node, in that way even for new content need prepare new presenter
        protected override void ChangeRegion(IRegionView regionView, object content, string? presenterKey = null)
        {
            PresenterData? presenter;
            // regionView don't contains any presenter, so add new
            if (regionView.CurrentData is not { } currentViewData)
            {
                presenter = presenterKey is null
                    ? CreatePresenterData(content)
                    : CreatePresenterData(content, presenterKey);
                regionView.Change(presenter);
                AddNewPresenter(presenter);
                return;
            }

            // PresenterKey not null
            if (presenterKey is not null)
            {
                // Check that content is same as in previous presenter
                if (currentViewData.Content.GetHashCode() == content.GetHashCode())
                {
                    // do nothing if presenter is same
                    if (currentViewData.PresenterKey == presenterKey) return;

                    // change presenter and add it to cache, because we change presenter for current content
                    presenter = CreatePresenterData(content, presenterKey);
                    regionView.Change(presenter);
                    AddNewPresenter(presenter);
                    return;
                }

                // if presenter for new content same just switch content
                if (currentViewData.PresenterKey == presenterKey)
                {
                    regionView.ChangeContent(content);
                    return;
                }

                // change presenter and add it to cache, because presenter must be changed
                presenter = CreatePresenterData(content, presenterKey);
                regionView.Change(presenter);
                AddNewPresenter(presenter);
                return;
            }

            // PresenterKey is null
            // if content not changed, do nothing
            if (currentViewData.Content.GetHashCode() == content.GetHashCode()) return;
            // if template still valuable, change only content
            if (content.GetType() == currentViewData.Template.DataType as Type)
            {
                regionView.ChangeContent(content);
                return;
            }

            // Content was changed and current template cannot handle it, so find and add new
            presenter = CreatePresenterData(content);
            regionView.Change(presenter);
            AddNewPresenter(presenter);
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
            regionView.ClearView();
        }

        private void AddNewPresenter(PresenterData presenter)
        {
            _dataCache.Add(presenter);
            _index++;
        }
    }
}