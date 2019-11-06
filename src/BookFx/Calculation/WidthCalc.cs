namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;
    using static MinWidthCalc;

    internal static class WidthCalc
    {
        public static Sc<Cache, int> Width(BoxCore box, Structure structure) =>
            throw new NotImplementedException();

        private static Sc<Cache, int> OfValue(BoxCore box, Structure structure) =>
            box
                .RowSpan
                .Map(ScOf)
                .OrElse(() => structure
                    .Parent(box)
                    .Map(
                        row: _ => ScOf(1),
                        col: MinWidth,
                        stack: MinWidth,
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => ScOf(1)))
                .GetOrElse(ScOf(1));
    }
}