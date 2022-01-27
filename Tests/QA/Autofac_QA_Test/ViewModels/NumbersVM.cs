using System;
using Thundire.MVVM.Core.Observable;

namespace Autofac_QA_Test.ViewModels
{
    public class NumbersVM : NotifyBase, IEquatable<NumbersVM>
    {
        private int _number1;
        public int Number1 { get => _number1; set => Set(ref _number1, value); }
        private int _number2;
        public int Number2 { get => _number2; set => Set(ref _number2, value); }

        public override bool Equals(object obj)
        {
            return Equals(obj as NumbersVM);
        }

        public bool Equals(NumbersVM other)
        {
            return other != null &&
                   _number1 == other._number1 &&
                   _number2 == other._number2 ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_number1, _number2);
        }
    }
}