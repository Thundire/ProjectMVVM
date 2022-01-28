﻿using Autofac;

using Autofac_QA_Test.Models;

using System.Collections.Generic;
using System.Linq;

using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.PagesNavigator;

namespace Autofac_QA_Test.ViewModels
{
    public class NavigationVM : NotifyBase, INavigationRoot
    {
        private readonly ILifetimeScope _provider;

        public NavigationVM(INavigator navigator, INavigationGroupDescriptors pages, ILifetimeScope provider)
        {
            _provider = provider;
            Navigator = navigator;
            Pages = pages.Pages;
        }

        public INavigator Navigator { get; }
        public IReadOnlyCollection<NavigationDescriptor> Pages { get; }

        private NavigationDescriptor _selected;
        public NavigationDescriptor Selected
        {
            get => _selected;
            set => Set(ref _selected, value)
                .DoOnSuccess(state =>
                {
                    if (state.New is not { } descriptor)
                    {
                        if (state.Previous is null) return;
                        Selected = state.Previous;
                        return;
                    }

                    Navigate(descriptor);
                });
        }

        public void SetDefaultPage(string pageKey)
        {
            if (Pages.Count <= 0) return;
            var descriptor = Pages.FirstOrDefault(descriptor => descriptor.PageKey == pageKey);
            if (descriptor is null) Selected = Pages.First();
            Selected = descriptor;
        }

        private void Navigate(NavigationDescriptor descriptor)
        {
            var pageKey = descriptor.PageKey;
            var dataContext = descriptor.DataContext;
            var dataContextType = descriptor.DataContextType;

            if (Navigator.CurrentPageKey == pageKey)
            {
                if (ReferenceEquals(dataContext, Navigator.CurrentDataContext)) return;

                if (dataContext is not null)
                {
                    Navigator.ChangeDataContextOfCurrentPage(dataContext);
                }
            }

            if (dataContext is null && dataContextType is not null)
            {
                dataContext = _provider.Resolve(dataContextType);
                descriptor.DataContext = dataContext;
            }

            Navigator.NavigateTo(pageKey, dataContext);
        }
    }
}