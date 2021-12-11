using Moq;

using System;
using System.Windows;
using Thundire.MVVM.WPF.Abstractions.TemplatesCache;
using Thundire.MVVM.WPF.Services.Interfaces;
using Thundire.MVVM.WPF.Services.Regions;
using Thundire.MVVM.WPF.Services.Regions.Interfaces;
using Thundire.MVVM.WPF.Services.Regions.Models;

using Xunit;

namespace Thundire.MVVM.WPF.UnitTests.RegionService
{
    public class StackViewsRegionTests
    {
        private StackViewsRegion Region { get; }
        private Mock<IRegionView> RegionView { get; }
        private Mock<ITemplatesCache> TemplatesCacheMock { get; }

        public StackViewsRegionTests()
        {
            TemplatesCacheMock = new();
            RegionView = new();
            Region = new(TemplatesCacheMock.Object) { RegionView = RegionView.Object };

            RegionView.Setup(view => view.Change(It.IsNotNull<PresenterData>())).Verifiable();
            RegionView.Setup(view => view.Close()).Verifiable();
            RegionView.Setup(view => view.Show()).Verifiable();
        }

        [Fact]
        public void Change_ChangeView_WhenTemplateFound()
        {
            object viewModel = 1;
            TemplatesCacheMock
                .Setup(cache => cache.GetTemplate(
                    It.Is<object>(s => s == viewModel),
                    It.IsAny<string>()))
                .Returns(() => new());

            Region.Change(viewModel);

            RegionView.Verify(view => view.Change(It.IsNotNull<PresenterData>()), Times.Once());
        }

        [Fact]
        public void Change_UpdateContent_WhenViewHasCurrentView()
        {
            object viewModel = 1;
            var presenter = new PresenterData(viewModel, new(typeof(int)));
            TemplatesCacheMock
                .Setup(cache => cache.GetTemplate(
                    It.Is<object>(s => s == viewModel),
                    It.IsAny<string>()))
                .Returns(() => presenter.Template);

            RegionView.SetupGet(view => view.CurrentData).Returns(presenter);

            Region.Change(viewModel);

            RegionView.Verify(view => view.Change(It.IsNotNull<PresenterData>()), Times.Never());
            RegionView.Verify(view => view.ChangeContent(It.IsNotNull<object>()), Times.Once());
        }

        [Fact]
        public void Change_ThrowException_WhenTemplateNotFound()
        {
            Assert.Throws<InvalidOperationException>(() => Region.Change(1));
        }

        [Fact]
        public void Close_CloseViewAndClearCurrentViewInRegionView_WhenRegionNotHasStackedViews()
        {
            Region.Close();

            RegionView.Verify(view => view.Close(), Times.Once());
            RegionView.Verify(view => view.Change(null), Times.Once());
        }

        [Fact]
        public void Open_OpenView()
        {
            object viewModel = 1;
            var presenter = new PresenterData(viewModel, new());
            TemplatesCacheMock
                .Setup(cache => cache.GetTemplate(
                    It.Is<object>(s => s == viewModel),
                    It.IsAny<string>()))
                .Returns(() => presenter.Template);
            RegionView.SetupGet(view => view.CurrentData).Returns(presenter);

            Region.Change(viewModel);
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