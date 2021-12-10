using System;

namespace Thundire.Helpers
{
    /// <summary>
    /// Represents a void type, since <see cref="System.Void"/> is not a valid return type in C#.
    /// </summary>
    public readonly struct VoidUnit : IEquatable<VoidUnit>, IComparable<VoidUnit>, IComparable
    {
        // ReSharper disable once InconsistentNaming
        private static readonly VoidUnit _value = new();
        public static ref readonly VoidUnit Value => ref _value;

        public bool Equals(VoidUnit other) => true;

        public int CompareTo(VoidUnit other) => 0;

        int IComparable.CompareTo(object? obj) => 0;

        public override bool Equals(object? obj) => obj is VoidUnit;

        public override int GetHashCode() => 0;
        public override string ToString() => "()";

        public static bool operator ==(VoidUnit left, VoidUnit right) => left.Equals(right);

        public static bool operator !=(VoidUnit left, VoidUnit right) => !(left == right);

        public static bool operator <(VoidUnit left, VoidUnit right) => left.CompareTo(right) < 0;

        public static bool operator <=(VoidUnit left, VoidUnit right) => left.CompareTo(right) <= 0;

        public static bool operator >(VoidUnit left, VoidUnit right) => left.CompareTo(right) > 0;

        public static bool operator >=(VoidUnit left, VoidUnit right) => left.CompareTo(right) >= 0;
    }
}