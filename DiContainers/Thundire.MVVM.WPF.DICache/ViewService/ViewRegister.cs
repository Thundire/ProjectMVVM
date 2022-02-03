using System.Collections.Generic;

using Thundire.Core.DIContainer;
using Thundire.MVVM.WPF.Abstractions.ViewService;
using Thundire.MVVM.WPF.Core.DICache.ViewService;

namespace Thundire.MVVM.WPF.DICache.ViewService
{
    public class ViewRegister : IViewRegister
    {
        private readonly IDIContainerBuilder _container;
        private readonly List<ViewRegistrationDescriptor> _cache = new();
        private readonly List<IViewRegistration> _builders = new();

        public ViewRegister(IDIContainerBuilder builder) => _container = builder;

        public IViewRegistrationBuilder Register<TView>(string mark) where TView : IView
        {
            var builder = new ViewRegistrationBuilder(typeof(TView), mark);
            _builders.Add(builder.AsRegistration());
            return builder;
        }

        public IViewRegistrationBuilder Register<TView, TViewModel>(string mark) where TView : IView
        {
            var builder = new ViewRegistrationBuilder(typeof(TView), mark).WithViewModel<TViewModel>();
            _builders.Add(builder.AsRegistration());
            return builder;
        }

        public IReadOnlyList<ViewRegistrationDescriptor> Build()
        {
            foreach (var builder in _builders)
            {
                _container.RegisterType(builder.View, builder.ViewMode);
                if (builder.HasViewModel) _container.RegisterType(builder.ViewModel!, builder.ViewModelMode);
                _cache.Add(builder.Build());
            }

            return _cache;
        }
    }
}