namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static HeightCalc;

    internal static class FirstRowCalc
    {
        public static int FirstRow(BoxCore box, Structure structure, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.FirstRow),
                f: () => OfBox(box, structure, cache));

        private static int OfBox(BoxCore box, Structure structure, Cache cache) =>
            structure
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => FirstRow(parent, structure, cache),
                        col: _ => structure
                            .Prev(box)
                            .Map(prev => FirstRow(prev, structure, cache) + Height(prev, structure, cache))
                            .GetOrElse(() => FirstRow(parent, structure, cache)),
                        stack: _ => FirstRow(parent, structure, cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            FirstRow(parent, structure, cache) + structure.Slot(box).Position.ValueUnsafe().Row - 1
                    )
                )
                .GetOrElse(1);
    }
}