namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static MinHeightCalc;

    internal static class HeightCalc
    {
        public static int Height(BoxCore box, Relations relations, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.Hight),
                f: () => box.Match(
                    row: _ => OfComposite(box, cache),
                    col: _ => OfComposite(box, cache),
                    stack: _ => OfComposite(box, cache),
                    value: _ => OfValue(box, relations, cache),
                    proto: _ => OfComposite(box, cache)));

        private static int OfComposite(BoxCore box, Cache cache) => MinHeight(box, cache);

        private static int OfValue(BoxCore box, Relations relations, Cache cache) =>
            box
                .RowSpan
                .OrElse(() => relations
                    .Parent(box)
                    .Map(
                        row: parent => MinHeight(parent, cache),
                        col: _ => 1,
                        stack: parent => MinHeight(parent, cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => 1))
                .GetOrElse(1);
    }
}