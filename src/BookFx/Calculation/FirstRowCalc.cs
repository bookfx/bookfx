namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static HeightCalc;

    internal static class FirstRowCalc
    {
        public static int FirstRow(BoxCore box, Relations relations, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.FirstRow),
                f: () => OfBox(box, relations, cache));

        private static int OfBox(BoxCore box, Relations relations, Cache cache) =>
            relations
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => FirstRow(parent, relations, cache),
                        col: _ => relations
                            .Prev(box)
                            .Map(prev => FirstRow(prev, relations, cache) + Height(prev, relations, cache))
                            .GetOrElse(() => FirstRow(parent, relations, cache)),
                        stack: _ => FirstRow(parent, relations, cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            FirstRow(parent, relations, cache) + relations.Slot(box).Position.ValueUnsafe().Row - 1
                    )
                )
                .GetOrElse(1);
    }
}