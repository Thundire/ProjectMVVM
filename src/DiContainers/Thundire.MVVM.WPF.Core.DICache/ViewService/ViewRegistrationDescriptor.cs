using System;

using Thundire.MVVM.WPF.Abstractions.ViewService;

namespace Thundire.MVVM.WPF.Core.DICache.ViewService
{
    public class ViewRegistrationDescriptor
    {
        public string Mark { get; init; }
        public Type View { get; init; }
        public Type? DataContext { get; init; }
        public bool HasDataContext => DataContext is not null;

        public ViewRegistrationDescriptor(string mark, Type view, Type? viewModel = null)
        {
            if (!IsView(view)) throw new ArgumentException("View type must inherit IView interface", nameof(view)) { Data = { [nameof(view)] = view } };

            Mark = mark;
            View = view;
            DataContext = viewModel;
        }

        private static bool IsView(Type view) =>
            view is null
                ? throw new ArgumentNullException(nameof(view), "Type of view was null")
                : view.GetInterface(nameof(IView)) is not null;

    }
}