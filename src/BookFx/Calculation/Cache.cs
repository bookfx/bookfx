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

        private enum Measure
        {
            FirstRow,
            FirstCol,
            Height,
            Width,
            MinHeight,
            MinWidth,
        }

        public static Cache Create(int boxCount) => new Cache(boxCount);

        public int FirstRow(BoxCore box, Func<int> f) => GetOrCompute(box, Measure.FirstRow, f);

        public int FirstCol(BoxCore box, Func<int> f) => GetOrCompute(box, Measure.FirstCol, f);

        public int Height(BoxCore box, Func<int> f) => GetOrCompute(box, Measure.Height, f);

        public int Width(BoxCore box, Func<int> f) => GetOrCompute(box, Measure.Width, f);

        public int MinHeight(BoxCore box, Func<int> f) => GetOrCompute(box, Measure.MinHeight, f);

        public int MinWidth(BoxCore box, Func<int> f) => GetOrCompute(box, Measure.MinWidth, f);

        private int GetOrCompute(BoxCore box, Measure measure, Func<int> f) =>
            _values[box.Number, (int)measure]
                .Match(
                    none: () =>
                    {
                        var value = f();
                        _values[box.Number, (int)measure] = value;
                        return value;
                    },
                    some: value => value);
    }
}