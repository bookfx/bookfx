namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;

    internal static class MinWidthCalc
    {
        public static Sc<Cache, int> MinWidth(BoxCore box) =>
            cache => cache.GetOrCompute(
                key: (box, Measure.MinWidth),
                sc: () => box.Match(
                    row: _ => OfRow(box),
                    col: _ => OfCol(box),
                    stack: _ => OfStack(box),
                    value: _ => OfValue(box),
                    proto: _ => OfProto(box)));

        private static Sc<Cache, int> OfRow(BoxCore box) =>
            box.Children.Map(MinWidth).Compose(seed: 0, (x, y) => x + y);

        private static Sc<Cache, int> OfCol(BoxCore box) =>
            box.Children.Map(MinWidth).Compose(seed: 0, Math.Max);

        private static Sc<Cache, int> OfStack(BoxCore box) =>
            box.Children.Map(MinWidth).Compose(seed: 0, Math.Max);

        private static Sc<Cache, int> OfValue(BoxCore box) =>
            ScOf(box.ColSpan.GetOrElse(1));

        private static Sc<Cache, int> OfProto(BoxCore box) =>
            ScOf(box.Proto.Bind(x => x.Range).ValueUnsafe().Columns);
    }
}