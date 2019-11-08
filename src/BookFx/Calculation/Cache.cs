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
        private readonly Option<int>[,] _ints;
        private readonly Option<bool>[,] _bools;

        private Cache(int boxCount)
        {
            _ints = new Option<int>[boxCount, Enum.GetValues(typeof(IntMeasure)).Length];
            _bools = new Option<bool>[boxCount, Enum.GetValues(typeof(BoolMeasure)).Length];
        }

        private enum IntMeasure
        {
            FirstRow,
            FirstCol,
            Height,
            Width,
            MinHeight,
            MinWidth,
        }

        private enum BoolMeasure
        {
            CanGrowHeight,
            CanGrowWeight,
            ShouldGrowHeight,
            ShouldGrowWeight,
        }

        public static Cache Create(int boxCount) => new Cache(boxCount);

        public int FirstRow(BoxCore box, Func<int> f) => GetOrCompute(box, IntMeasure.FirstRow, f);

        public int FirstCol(BoxCore box, Func<int> f) => GetOrCompute(box, IntMeasure.FirstCol, f);

        public int Height(BoxCore box, Func<int> f) => GetOrCompute(box, IntMeasure.Height, f);

        public int Width(BoxCore box, Func<int> f) => GetOrCompute(box, IntMeasure.Width, f);

        public int MinHeight(BoxCore box, Func<int> f) => GetOrCompute(box, IntMeasure.MinHeight, f);

        public int MinWidth(BoxCore box, Func<int> f) => GetOrCompute(box, IntMeasure.MinWidth, f);

        public bool CanGrowHeight(BoxCore box, Func<bool> f) => GetOrCompute(box, BoolMeasure.CanGrowHeight, f);

        public bool CanGrowWeight(BoxCore box, Func<bool> f) => GetOrCompute(box, BoolMeasure.CanGrowWeight, f);

        public bool ShouldGrowHeight(BoxCore box, Func<bool> f) => GetOrCompute(box, BoolMeasure.ShouldGrowHeight, f);

        public bool ShouldGrowWeight(BoxCore box, Func<bool> f) => GetOrCompute(box, BoolMeasure.ShouldGrowWeight, f);

        private static T GetOrCompute<T>(Option<T>[,] values, BoxCore box, int measure, Func<T> f) =>
            values[box.Number, measure]
                .Match(
                    none: () =>
                    {
                        var value = f();
                        values[box.Number, measure] = value;
                        return value;
                    },
                    some: value => value);

        private int GetOrCompute(BoxCore box, IntMeasure measure, Func<int> f) =>
            GetOrCompute(_ints, box, (int)measure, f);

        private bool GetOrCompute(BoxCore box, BoolMeasure measure, Func<bool> f) =>
            GetOrCompute(_bools, box, (int)measure, f);
    }
}