using System;
using System.Collections.Generic;
using System.Linq;

using Thundire.Core.DIContainer;
using Thundire.MVVM.WPF.Abstractions.ViewService;
using Thundire.MVVM.WPF.Core.DICache.ViewService;

namespace Thundire.MVVM.WPF.DICache.ViewService
{
    public class ViewRegisterCache : IViewRegisterCache
    {
        private readonly IDIContainer _container;
        private readonly IReadOnlyList<ViewRegistrationDescriptor> _registrations;
        private readonly HashSet<string> _registeredMarks;

        public ViewRegisterCache(IDIContainer container, IReadOnlyList<ViewRegistrationDescriptor> registrations)
        {
            _container = container;
            _registrations = registrations;
            _registeredMarks = registrations.Select(r => r.Mark).ToHashSet();
        }

        public bool IsMarkNotRegistered(string mark) => !_registeredMarks.Contains(mark);

        public ViewDescriptor GetView(string mark)
        {
            var registration = _registrations.FirstOrDefault(r => r.Mark == mark);
            if (registration is null) throw new InvalidOperationException("Mark was not registered");

            var view = _container.Resolve(registration.View);
            if (view is not IView subscriber)
                throw new InvalidOperationException($"Invalid registration of view marked as {mark}");

            if (!registration.HasDataContext) return new ViewDescriptor(subscriber);

            var dataContext = _container.Resolve(registration.DataContext!);
            subscriber.DataContext = dataContext;

            return new ViewDescriptor(subscriber, dataContext);
        }
    }
}