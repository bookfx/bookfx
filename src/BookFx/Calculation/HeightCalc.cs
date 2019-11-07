namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static MinHeightCalc;

    internal static class HeightCalc
    {
        public static int Height(BoxCore box, Structure structure, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.Hight),
                f: () => box.Match(
                    row: _ => OfComposite(box, cache),
                    col: _ => OfComposite(box, cache),
                    stack: _ => OfComposite(box, cache),
                    value: _ => OfValue(box, structure, cache),
                    proto: _ => OfComposite(box, cache)));

        private static int OfComposite(BoxCore box, Cache cache) => MinHeight(box, cache);

        private static int OfValue(BoxCore box, Structure structure, Cache cache) =>
            box
                .RowSpan
                .OrElse(() => structure
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