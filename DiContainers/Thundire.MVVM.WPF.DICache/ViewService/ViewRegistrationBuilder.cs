using System;

using Thundire.Core.DIContainer;
using Thundire.MVVM.WPF.Abstractions.ViewService;
using Thundire.MVVM.WPF.Core.DICache.ViewService;

namespace Thundire.MVVM.WPF.DICache.ViewService
{
    internal class ViewRegistrationBuilder : IViewRegistrationBuilder, IViewRegistration
    {
        public Type View { get; }
        public Type? ViewModel { get; private set; }
        private string Mark { get; set; }
        public LifeTimeMode ViewMode { get; private set; } = LifeTimeMode.Transient;
        public LifeTimeMode ViewModelMode { get; private set; } = LifeTimeMode.Transient;
        public bool HasViewModel => ViewModel is not null;

        public ViewRegistrationBuilder(Type view, string mark)
        {
            if (!view.IsAssignableTo(typeof(IView)))
                throw new ArgumentException("View must inherit from IView");
            View = view;
            Mark = mark;
        }

        public IViewRegistrationBuilder WithViewModel<TViewModel>()
        {
            ViewModel = typeof(TViewModel);
            return this;
        }

        public IViewRegistrationBuilder ViewAsSingleton()
        {
            ViewMode = LifeTimeMode.Singleton;
            return this;
        }

        public IViewRegistrationBuilder ViewAsTransient()
        {
            ViewMode = LifeTimeMode.Transient;
            return this;
        }

        public IViewRegistrationBuilder ViewModelAsSingleton()
        {
            ViewModelMode = LifeTimeMode.Singleton;
            return this;
        }

        public IViewRegistrationBuilder ViewModelAsTransient()
        {
            ViewModelMode = LifeTimeMode.Transient;
            return this;
        }

        public ViewRegistrationDescriptor Build() => new(Mark, View, ViewModel);

        public IViewRegistration AsRegistration() => this;
    }
}