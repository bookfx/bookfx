namespace BookFx.Calculation
{
    using System.Collections.Immutable;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;

    /// <summary>
    /// Cache for layout calculations.
    /// </summary>
    internal class Cache
    {
        public static readonly Cache Empty = new Cache(ImmutableDictionary<(BoxCore Box, Measure Measure), int>.Empty);

        private readonly ImmutableDictionary<(BoxCore Box, Measure Measure), int> _dictionary;

        private Cache(ImmutableDictionary<(BoxCore Box, Measure Measure), int> dictionary) => _dictionary = dictionary;

        [Pure]
        public Cache Add((BoxCore Box, Measure Measure) key, int value) => new Cache(_dictionary.Add(key, value));

        [Pure]
        public Option<int> TryGetValue((BoxCore Box, Measure Measure) key) => _dictionary.TryGetValue(key);
    }
}