using System.Collections.Generic;
using Thundire.MVVM.WPF.Abstractions.DependencyInjection;
using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.ViewService
{
    public class ViewRegister : IViewRegister, IViewRegisterCache
    {
        private readonly IDIContainerBuilder _container;
        private readonly List<ViewRegistration> _cache = new();
        private readonly List<IViewRegistration> _builders = new();

        public ViewRegister(IDIContainerBuilder builder) => _container = builder;

        public IReadOnlyList<ViewRegistration> Registrations => _cache;

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

        public void Build()
        {
            foreach (var builder in _builders)
            {
                _container.RegisterType(builder.View, builder.ViewMode);
                if(builder.HasViewModel) _container.RegisterType(builder.ViewModel!, builder.ViewModelMode);
                _cache.Add(builder.Build());
            }
        }
    }
}