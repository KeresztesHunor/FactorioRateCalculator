using System.Numerics;

namespace FactorioRateCalculator
{
    internal static class Utils
    {
        public static T GreatestCommonDenominator<T>(T a, T b) where T : struct, IBinaryInteger<T>
        {
            while (b != T.Zero)
            {
                T tmp = b;
                b = a % b;
                a = tmp;
            }
            return T.Abs(a);
        }

        public static T SmallestCommonMultiple<T>(T a, T b) where T : struct, IBinaryInteger<T> => a != T.Zero && b != T.Zero ? T.Abs(a * b) / GreatestCommonDenominator(a, b) : T.Zero;

        public static Rational<T> RecalculateWithNewDenominator<T>(this Rational<T> value, T newDenominator, bool simplifyFirst = true) where T : struct, IBinaryInteger<T>, ISignedNumber<T>
        {
            if (simplifyFirst)
            {
                value = value.Simplified;
            }
            if (newDenominator <= T.Zero || newDenominator % value.Denominator != T.Zero)
            {
                throw new ArithmeticException($"Cannot recalculate {value.ToString()} to the following new denominator: {newDenominator}");
            }
            return new Rational<T>(
                value.Enumerator * (value.Denominator == newDenominator ? newDenominator : newDenominator / value.Denominator),
                !value.isNegative ? newDenominator : -newDenominator
            );
        }

        public static string PascalToKebabCase(this string s) => s.ToCase(char.ToLower, char.IsUpper, static (char c) => "-" + c);

        public static string KebabToPascalCase(this string s) => s.ToCase(char.ToUpper, static (char c) => c == '-', null, (ref int i) => ++i);

        static string ToCase(this string s, Func<char, char> toCase, Func<char, bool> condition, Func<char, string>? modifier = null, Referencer<int>? indexer = null)
        {
            string result = toCase(s[0]).ToString();
            for (int i = 1; i < s.Length; i++)
            {
                result += condition(s[i]) ? modifier?.Invoke(GetChar()) ?? GetChar().ToString() : s[i];

                char GetChar() => toCase(s[indexer?.Invoke(ref i) ?? i]);
            }
            return result;
        }

        delegate T Referencer<T>(ref T value) where T : struct;
    }
}
