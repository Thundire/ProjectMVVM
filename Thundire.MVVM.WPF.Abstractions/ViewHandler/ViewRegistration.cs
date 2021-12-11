using System;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public class ViewRegistration
    {
        public string Mark { get; init; }
        public Type View { get; init; }
        public Type ViewModel { get; init; }
        public bool ViewModelRequired { get; init; }


        public ViewRegistration(Type view) : this(string.Empty, view)
        {
        }

        public ViewRegistration(string mark, Type view, Type viewModel = null)
        {
            if (!IsView(view)) throw new ArgumentException(null, nameof(view)) { Data = { [nameof(view)] = view } };

            Mark = mark;
            View = view;
            ViewModel = viewModel;
            ViewModelRequired = viewModel is not null;
        }


        private static bool IsView(Type view) =>
            view is null
                ? throw new ArgumentNullException(nameof(view)) { Data = { [nameof(view)] = null } }
                : view.GetInterface(nameof(IView)) is not null;

    }
}