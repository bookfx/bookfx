namespace BookFx.Epplus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    internal static class ColumnConverter
    {
        private const int Base = 26;
        private static readonly Regex NameRegex = new Regex("(?i)^[a-z]{1,3}$");

        [Pure]
        public static Option<string> NumberToName(int number) =>
            Some(number)
                .Where(n => n >= 1 && n <= Constraint.MaxColumn)
                .Map(n => GetDigits(n)
                    .Reverse()
                    .Map(digit => (char)('A' + digit))
                    .Join());

        [Pure]
        public static Option<int> NameToNumber(string name) =>
            Some(name)
                .Where(NameRegex.IsMatch)
                .Map(s => s.ToUpper())
                .Map(s => s.Map(c => c - 'A').Aggregate(0, (acc, digit) => (acc * Base) + digit + 1))
                .Where(n => n <= Constraint.MaxColumn);

        private static IEnumerable<int> GetDigits(int number)
        {
            var excess = number;

            while (excess > 0)
            {
                var digit = (excess - 1) % Base;
                yield return digit;
                excess = (excess - digit) / Base;
            }
        }
    }
}