namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static WidthCalc;

    internal static class FirstColCalc
    {
        public static int FirstCol(BoxCore box, Structure structure, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.FirstCol),
                f: () => OfBox(box, structure, cache));

        private static int OfBox(BoxCore box, Structure structure, Cache cache) =>
            structure
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => structure
                            .Prev(box)
                            .Map(prev => FirstCol(prev, structure, cache) + Width(prev, structure, cache))
                            .GetOrElse(() => FirstCol(parent, structure, cache)),
                        col: _ => FirstCol(parent, structure, cache),
                        stack: _ => FirstCol(parent, structure, cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            FirstCol(parent, structure, cache) + structure.Slot(box).Position.ValueUnsafe().Col - 1
                    )
                )
                .GetOrElse(1);
    }
}