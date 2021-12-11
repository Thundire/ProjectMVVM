using System;

using Thundire.MVVM.WPF.Abstractions.DependencyInjection;
using Thundire.MVVM.WPF.Abstractions.ViewHandler;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    internal class ViewRegistrationBuilder : IViewRegistrationBuilder, IViewRegistration
    {
        public Type View { get; }
        public Type ViewModel { get; private set; }
        private string Mark { get; set; }
        public LifeTimeMode ViewMode { get; private set; } = LifeTimeMode.Transient;
        public LifeTimeMode ViewModelMode { get; private set; } = LifeTimeMode.Transient;

        public ViewRegistrationBuilder(Type view)
        {
            if (!view.IsAssignableTo(typeof(IView)))
                throw new ArgumentException("View must inherit from IView");
            View = view;
        }

        public IViewRegistrationBuilder WithViewModel<TViewModel>()
        {
            ViewModel = typeof(TViewModel);
            return this;
        }

        public IViewRegistrationBuilder MarkAs(string mark)
        {
            Mark = mark;
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

        public ViewRegistration Build() => new(Mark, View, ViewModel);

        public IViewRegistration AsRegistration() => this;
    }
}