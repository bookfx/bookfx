namespace BookFx.Functional
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using static F;

    internal static class DictionaryExt
    {
        [Pure]
        public static Option<TR> TryGetValue<TKey, TR>(this IReadOnlyDictionary<TKey, TR> dict, TKey key) =>
            dict.TryGetValue(key, out var value) ? Some(value) : None;
    }
}