namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static WidthCalc;

    internal static class FirstColCalc
    {
        public static int FirstCol(BoxCore box, Relations relations, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.FirstCol),
                f: () => OfBox(box, relations, cache));

        private static int OfBox(BoxCore box, Relations relations, Cache cache) =>
            relations
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => relations
                            .Prev(box)
                            .Map(prev => FirstCol(prev, relations, cache) + Width(prev, relations, cache))
                            .GetOrElse(() => FirstCol(parent, relations, cache)),
                        col: _ => FirstCol(parent, relations, cache),
                        stack: _ => FirstCol(parent, relations, cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            FirstCol(parent, relations, cache) + relations.Slot(box).Position.ValueUnsafe().Col - 1
                    )
                )
                .GetOrElse(1);
    }
}