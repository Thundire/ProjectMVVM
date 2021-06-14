using System;
using System.Collections.Generic;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    public class ViewRegister : IViewRegister, IViewRegisterCache
    {
        private readonly IDIContainerBuilder _builder;
        private readonly List<ViewRegistration> _cache = new();
        private readonly List<ViewRegistrationBuilder> _builders = new();

        public ViewRegister(IDIContainerBuilder builder)
        {
            _builder = builder;
        }
        
        public IReadOnlyList<ViewRegistration> Registrations => _cache;
        
        public ViewRegistrationBuilder Register<TView>(string mark) where TView : IView
        {
            var builder = new ViewRegistrationBuilder(typeof(TView)).MarkAs(mark);
            _builders.Add(builder);
            return builder;
        }

        public ViewRegistrationBuilder Register<TView, TViewModel>(string mark) where TView : IView
        {
            var builder = new ViewRegistrationBuilder(typeof(TView)).MarkAs(mark).WithViewModel<TViewModel>();
            _builders.Add(builder);
            return builder;
        }

        public void Build()
        {
            foreach (var builder in _builders)
            {
                _builder.RegisterType(builder.View, builder.ViewMode);
                _builder.RegisterType(builder.ViewModel, builder.ViewModelMode);
                _cache.Add(builder.Build());
            }
        }
        
        public class ViewRegistrationBuilder
        {
            internal Type View { get; }
            internal Type ViewModel { get; private set; }
            private string Mark { get; set; }
            internal LifeTimeMode ViewMode { get; private set; } = LifeTimeMode.Transient;
            internal LifeTimeMode ViewModelMode { get; private set; } = LifeTimeMode.Transient;

            public ViewRegistrationBuilder(Type view)
            {
                if (!view.IsAssignableTo(typeof(IView)))
                    throw new ArgumentException("View must inherit from IView");
                View = view;
            }

            public ViewRegistrationBuilder WithViewModel<TViewModel>()
            {
                ViewModel = typeof(TViewModel);
                return this;
            }

            public ViewRegistrationBuilder MarkAs(string mark)
            {
                Mark = mark;
                return this;
            }

            public ViewRegistrationBuilder ViewAsSingleton()
            {
                ViewMode = LifeTimeMode.Singleton;
                return this;
            }

            public ViewRegistrationBuilder ViewAsTransient()
            {
                ViewMode = LifeTimeMode.Transient;
                return this;
            }

            public ViewRegistrationBuilder ViewModelAsSingleton()
            {
                ViewModelMode = LifeTimeMode.Singleton;
                return this;
            }

            public ViewRegistrationBuilder ViewModelAsTransient()
            {
                ViewModelMode = LifeTimeMode.Transient;
                return this;
            }

            internal ViewRegistration Build() => new(Mark, View, ViewModel);
        }
    }
}