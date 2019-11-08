namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static HeightCalc;

    internal static class FirstRowCalc
    {
        public static int FirstRow(BoxCore box, Layout layout) =>
            layout.Cache.GetOrCompute(
                key: (box, Measure.FirstRow),
                f: () => OfBox(box, layout));

        private static int OfBox(BoxCore box, Layout layout) =>
            layout
                .Relations
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => FirstRow(parent, layout),
                        col: _ => layout
                            .Relations
                            .Prev(box)
                            .Map(prev => FirstRow(prev, layout) + Height(prev, layout))
                            .GetOrElse(() => FirstRow(parent, layout)),
                        stack: _ => FirstRow(parent, layout),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            FirstRow(parent, layout) + layout.Relations.Slot(box).Position.ValueUnsafe().Row - 1
                    )
                )
                .GetOrElse(1);
    }
}