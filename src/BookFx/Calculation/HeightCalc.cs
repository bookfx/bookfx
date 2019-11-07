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
            cache => cache.GetOrCompute(
                key: (box, Measure.Hight),
                sc: () => box.Match(
                    row: _ => OfComposite(box),
                    col: _ => OfComposite(box),
                    stack: _ => OfComposite(box),
                    value: _ => OfValue(box, structure),
                    proto: _ => OfComposite(box)));

        private static Sc<Cache, int> OfComposite(BoxCore box) => MinHeight(box);

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
    }
}