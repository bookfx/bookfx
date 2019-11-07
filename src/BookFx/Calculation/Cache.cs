namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    /// <summary>
    /// Cache for layout calculations.
    /// </summary>
    internal class Cache
    {
        private readonly Option<int>[,] _values;

        private Cache(int boxCount) => _values = new Option<int>[boxCount, Enum.GetValues(typeof(Measure)).Length];

        public static Cache Create(int boxCount) => new Cache(boxCount);

        public (int Result, Cache Cache) GetOrCompute((BoxCore Box, Measure Measure) key, Func<Sc<Cache, int>> sc) =>
            _values[key.Box.Number, (int)key.Measure]
                .Match(
                    none: () =>
                    {
                        var (value, _) = sc()(this);
                        _values[key.Box.Number, (int)key.Measure] = value;
                        return (value, this);
                    },
                    some: value => (value, this));
    }
}