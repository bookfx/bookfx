namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class MinHeightCalc
    {
        public static Sc<Cache, int> MinHeight(BoxCore box) =>
            from cache in Sc<Cache>.Get
            from result in cache.GetOrCompute(
                key: (box, Measure.MinHight),
                computation: () => box.Match(
                    row: _ => OfRow(box),
                    col: _ => OfCol(box),
                    stack: _ => OfStack(box),
                    value: _ => OfValue(box),
                    proto: _ => OfProto(box)))
            select result;

        private static Sc<Cache, int> OfRow(BoxCore box) =>
            box.Children.Map(MinHeight).Compose(seed: 0, Math.Max);

        private static Sc<Cache, int> OfCol(BoxCore box) =>
            box.Children.Map(MinHeight).Compose(seed: 0, (x, y) => x + y);

        private static Sc<Cache, int> OfStack(BoxCore box) =>
            box.Children.Map(MinHeight).Compose(seed: 0, Math.Max);

        private static Sc<Cache, int> OfValue(BoxCore box) =>
            Sc<Cache>.Return(box.RowSpan.GetOrElse(1));

        private static Sc<Cache, int> OfProto(BoxCore box) =>
            Sc<Cache>.Return(box.Proto.Bind(x => x.Range).ValueUnsafe().Rows);
    }
}