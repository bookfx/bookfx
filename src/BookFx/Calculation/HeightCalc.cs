namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;
    using static MinHeightCalc;

    internal static class HeightCalc
    {
        public static Sc<Cache, int> Height(BoxCore box, Structure structure) => throw new NotImplementedException();

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