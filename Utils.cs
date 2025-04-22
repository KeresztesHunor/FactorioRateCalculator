namespace FactorioRateCalculator
{
    internal static class Utils
    {
        public static void ForEach<TEnumerable, T>(this TEnumerable values, Action<T> action) where TEnumerable : IEnumerable<T>
        {
            foreach (T item in values)
            {
                action(item);
            }
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
