// ReSharper disable once CheckNamespace
namespace System
{
    public static class BaseExtensions
    {
        /// <summary>
        /// Checking on null, WhiteSpace, Empty
        /// return result of string.IsNullOrEmpty(param) || string.IsNullOrWhiteSpace(param)
        /// </summary>
        /// <param name="str">String that must be checked</param>
        /// <returns>result of checking on null, WhiteSpace, Empty</returns>
        public static bool IsNotFilled(this string str) => string.IsNullOrWhiteSpace(str) is true || 0U >= (uint)str!.Length;

        /// <summary>
        /// Checking on null, WhiteSpace, Empty
        /// return result of string.IsNullOrEmpty(param) || string.IsNullOrWhiteSpace(param)
        /// </summary>
        /// <param name="str">String that must be checked</param>
        /// <returns>result of checking on null, WhiteSpace, Empty</returns>
        public static bool IsNotFilled(this string[] str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i].IsNotFilled()) return true;
            }

            return false;
        }

        /// <summary>
        /// Cut <paramref name="toCut"/> from current string, if it start with it
        /// </summary>
        /// <param name="self">Current String</param>
        /// <param name="toCut">A part at start of the string must be cut</param>
        /// <returns>Cut of current string or current string, if there nothing to cut</returns>
        public static string CutStart(this string self, string toCut) =>
            self.Length < toCut.Length && !self.StartsWith(toCut)
                ? self
                : self[toCut.Length..];

        /// <summary>
        /// Cut <paramref name="toCut"/> from current string, if it ends with it
        /// </summary>
        /// <param name="self">Current String</param>
        /// <param name="toCut">A part at end of the string must be cut</param>
        /// <returns>Cut of current string or current string, if there nothing to cut</returns>
        public static string CutEnd(this string self, string toCut)
        {
            if (self.Length < toCut.Length && !self.EndsWith(toCut)) return self;
            return self[..^toCut.Length];
        }

        /// <summary>
        /// Check equality of double values with tolerance precision
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="other">Other value</param>
        /// <param name="tolerance">Tolerance precision value</param>
        /// <returns>True if equality in tolerance precision, false if opposite</returns>
        public static bool ToleranceEquals(this double value, double other, double tolerance) => Math.Abs(value - other) < tolerance;

        /// <summary>
        /// Check equality of double values with tolerance precision
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="other">Other value</param>
        /// <param name="tolerance">Tolerance precision value</param>
        /// <returns>True if equality in tolerance precision, false if opposite</returns>
        public static bool ToleranceEquals(this float value, float other, float tolerance) => Math.Abs(value - other) < tolerance;

        /// <summary>
        /// Check equality of double values with tolerance precision
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="other">Other value</param>
        /// <param name="tolerance">Tolerance precision value</param>
        /// <returns>True if equality in tolerance precision, false if opposite</returns>
        public static bool ToleranceEquals(this decimal value, decimal other, decimal tolerance) => Math.Abs(value - other) < tolerance;
    }
}
