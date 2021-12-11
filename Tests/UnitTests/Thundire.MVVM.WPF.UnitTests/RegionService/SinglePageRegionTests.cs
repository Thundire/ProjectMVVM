using System;
using System.Windows;
using Moq;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;
using Thundire.MVVM.WPF.Services.Regions.Models;
using Xunit;

namespace Thundire.MVVM.WPF.UnitTests.RegionService
{
    public class SinglePageRegionTests
    {
        private SinglePageRegion Region { get; }
        private Mock<IRegionView> RegionView { get; }
        private Mock<ITemplatesCache> TemplatesCacheMock { get; }

        private const string DefaultPresenterKey = "ViewModel";
        private const string DefaultPresenterViewModel = "ViewModel";

        private static readonly DataTemplate DefaultPresenterTemplate = new(typeof(string));
        private static readonly PresenterData DefaultPresenter = new(DefaultPresenterViewModel, DefaultPresenterTemplate);

        public SinglePageRegionTests()
        {
            TemplatesCacheMock = new();
            RegionView = new();
            Region = new(TemplatesCacheMock.Object){RegionView = RegionView.Object};

            RegionView.Setup(view => view.Change(It.IsNotNull<PresenterData>())).Verifiable();
            RegionView.Setup(view => view.Close()).Verifiable();
            RegionView.Setup(view => view.Show()).Verifiable();

            TemplatesCacheMock
                .Setup(cache => cache.GetTemplate(
                    It.Is<string>(s => s == DefaultPresenterViewModel),
                    It.Is<string>(s => s == DefaultPresenterKey || s == null)))
                .Returns(() => DefaultPresenter.Template);
        }

        [Fact]
        public void Change_ChangeView_WhenTemplateFound()
        {
            Region.Change(DefaultPresenterViewModel);

            RegionView.Verify(view => view.Change(It.IsNotNull<PresenterData>()), Times.Once());
        }

        [Fact]
        public void Change_UpdateContent_WhenViewHasCurrentView()
        {
            RegionView.SetupGet(view => view.CurrentData).Returns(DefaultPresenter);
            Region.Change(DefaultPresenterViewModel);

            RegionView.Verify(view => view.Change(It.IsNotNull<PresenterData>()), Times.Never());
            RegionView.Verify(view => view.ChangeContent(It.IsNotNull<object>()), Times.Once());
        }

        [Fact]
        public void Change_ThrowException_WhenTemplateNotFound()
        {
            Assert.Throws<InvalidOperationException>(()=>Region.Change(1));
        }

        [Fact]
        public void Close_CloseView()
        {
            Region.Close();

            RegionView.Verify(view => view.Close(), Times.Once());
        }

        [Fact]
        public void Open_OpenView()
        {
            RegionView.SetupGet(view => view.CurrentData).Returns(DefaultPresenter);
            Region.Change(DefaultPresenterViewModel);
            Region.Open();

            RegionView.Verify(view => view.Show(), Times.Once());
        }

        [Fact]
        public void Open_ThrowException_WhenViewNotSet()
        {
            Assert.Throws<InvalidOperationException>(() => Region.Open());
            RegionView.Verify(view => view.Show(), Times.Never());
        }
    }
}