namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class FirstColCalc
    {
        public static int FirstCol(this BoxCore box, Layout layout) =>
            layout.Cache.GetOrCompute(
                key: (box, Measure.FirstCol),
                f: () => OfBox(box, layout));

        private static int OfBox(BoxCore box, Layout layout) =>
            layout
                .Relations
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => layout
                            .Relations
                            .Prev(box)
                            .Map(prev => prev.FirstCol(layout) + prev.Width(layout))
                            .GetOrElse(() => parent.FirstCol(layout)),
                        col: _ => parent.FirstCol(layout),
                        stack: _ => parent.FirstCol(layout),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            parent.FirstCol(layout) + layout.Relations.Slot(box).Position.ValueUnsafe().Col - 1
                    )
                )
                .GetOrElse(1);
    }
}