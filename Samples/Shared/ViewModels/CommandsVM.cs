using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Thundire.MVVM.Core.Observable;
using Thundire.MVVM.WPF.Abstractions.Commands;

#nullable enable
namespace Shared.ViewModels
{
    public class CommandsVM : NotifyBase
    {
        private static readonly Random Random = new Random();

        public CommandsVM(IWpfCommandsFactory commandsFactory)
        {
            _value = string.Empty;

            SimpleCommand = commandsFactory.Create(() => Value = "Hello World!");
            SimpleWithValidationCommand = commandsFactory.Create(() => Value = "Hello World! 2", () => IsValid);
            SimpleAsyncCommand = commandsFactory.Create(() => Task.Run(() => Dispatcher.CurrentDispatcher.Invoke(() => Value = "Hello World!")));
            SimpleAsyncWithValidationCommand = commandsFactory.Create(() => Task.Run(() => Dispatcher.CurrentDispatcher.Invoke(() => Value = "Hello World! 2")), () => IsValid);

            ModelSimpleCommand = commandsFactory.Create<Model?>(model =>
            {
                if (model is null)
                {
                    ModelValue = new("",1);
                    return;
                }
                model.String = Random.NextDouble().ToString(CultureInfo.InvariantCulture);
                model.Number = Random.Next(1, 7);
            }, parameterCanBeNull:true);

            ModelWithValidationCommand = commandsFactory.Create<Model?>(
                model =>
                {
                    if (model is not null) model.String = Random.NextDouble().ToString(CultureInfo.InvariantCulture);
                },
                model => model?.Number > 4);

            NumberSimpleCommand = commandsFactory.Create<int?>(i => Number += i, i => i is < 5 or null, parameterCanBeNull:true);
            NumberWithValidationCommand = commandsFactory.Create<int>(i => Number += i, i => i < 5);
        }

        private bool _isValid;
        public bool IsValid { get => _isValid; set => Set(ref _isValid, value); }

        private string _value;
        public string Value { get => _value; set => Set(ref _value, value); }

        private Model? _modelValue;
        public Model? ModelValue { get => _modelValue; set => Set(ref _modelValue, value); }

        private int? _number;
        public int? Number { get => _number; set => Set(ref _number, value); }

        public ICommand SimpleCommand { get; }
        public ICommand SimpleWithValidationCommand { get; }
        public ICommand SimpleAsyncCommand { get; }
        public ICommand SimpleAsyncWithValidationCommand { get; }
        public ICommand ModelSimpleCommand { get; }
        public ICommand ModelWithValidationCommand { get; }
        public ICommand NumberSimpleCommand { get; }
        public ICommand NumberWithValidationCommand { get; }

        public class Model : NotifyBase
        {
            private string _string;
            private int _number;

            public Model(string s, int number)
            {
                _string = s;
                _number = number;
            }

            public string String { get => _string; set => Set(ref _string, value); }
            public int Number { get => _number; set => Set(ref _number, value); }
        }
    }
}