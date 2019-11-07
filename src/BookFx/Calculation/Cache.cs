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

        public int GetOrCompute((BoxCore Box, Measure Measure) key, Func<int> f) =>
            _values[key.Box.Number, (int)key.Measure]
                .Match(
                    none: () =>
                    {
                        var value = f();
                        _values[key.Box.Number, (int)key.Measure] = value;
                        return value;
                    },
                    some: value => value);
    }
}