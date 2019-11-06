namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;
    using static MinHeightCalc;

    internal static class HeightCalc
    {
        public static Sc<Cache, int> Height(BoxCore box, Structure structure) =>
            from cache in GetState
            from result in cache.GetOrCompute(
                key: (box, Measure.Hight),
                sc: () => box.Match(
                    row: _ => OfRow(box),
                    col: _ => OfCol(box),
                    stack: _ => OfStack(box),
                    value: _ => OfValue(box, structure),
                    proto: _ => OfProto(box)))
            select result;

        private static Sc<Cache, int> OfRow(BoxCore box) => MinHeight(box);

        private static Sc<Cache, int> OfCol(BoxCore box) => MinHeight(box);

        private static Sc<Cache, int> OfStack(BoxCore box) => MinHeight(box);

        private static Sc<Cache, int> OfValue(BoxCore box, Structure structure) =>
            box
                .RowSpan
                .Map(ScOf)
                .OrElse(() => structure
                    .Parent(box)
                    .Map(
                        row: MinHeight,
                        col: _ => ScOf(1),
                        stack: MinHeight,
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => ScOf(1)))
                .GetOrElse(ScOf(1));

        private static Sc<Cache, int> OfProto(BoxCore box) => MinHeight(box);
    }
}