namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    internal static class StringExt
    {
        private const string IndentString = "    ";

        [Pure]
        public static string Join(this IEnumerable<string> strings, string separator = "") =>
            string.Join(separator, strings);

        [Pure]
        public static string Join(this IEnumerable<char> chars, string separator = "") =>
            string.Join(separator, chars);

        [Pure]
        public static string Indent(this string s) =>
            IndentString + s.Replace(Environment.NewLine, Environment.NewLine + IndentString);
    }
}