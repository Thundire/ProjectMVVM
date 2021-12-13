using System;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public class ViewRegistration
    {
        public string Mark { get; init; }
        public Type View { get; init; }
        public Type? ViewModel { get; init; }
        public bool HasViewModel => ViewModel is not null;


        public ViewRegistration(Type view) : this(string.Empty, view)
        {
        }

        public ViewRegistration(string mark, Type view, Type? viewModel = null)
        {
            if (!IsView(view)) throw new ArgumentException("View type must inherit IView interface", nameof(view)) { Data = { [nameof(view)] = view } };

            Mark = mark;
            View = view;
            ViewModel = viewModel;
        }


        private static bool IsView(Type view) =>
            view is null
                ? throw new ArgumentNullException(nameof(view), "Type of view was null")
                : view.GetInterface(nameof(IView)) is not null;

    }
}