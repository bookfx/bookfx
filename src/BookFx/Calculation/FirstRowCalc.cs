namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class FirstRowCalc
    {
        public static int FirstRow(this BoxCore box, Layout layout) =>
            layout.Cache.GetOrCompute(
                key: (box, Measure.FirstRow),
                f: () => OfBox(box, layout));

        private static int OfBox(BoxCore box, Layout layout) =>
            layout
                .Relations
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => parent.FirstRow(layout),
                        col: _ => layout
                            .Relations
                            .Prev(box)
                            .Map(prev => prev.FirstRow(layout) + prev.Height( layout))
                            .GetOrElse(() => parent.FirstRow(layout)),
                        stack: _ => parent.FirstRow(layout),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            parent.FirstRow(layout) + layout.Relations.Slot(box).Position.ValueUnsafe().Row - 1
                    )
                )
                .GetOrElse(1);
    }
}