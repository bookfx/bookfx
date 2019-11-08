namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static WidthCalc;

    internal static class FirstColCalc
    {
        public static int FirstCol(BoxCore box, Layout layout) =>
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
                            .Map(prev => FirstCol(prev, layout) + Width(prev, layout))
                            .GetOrElse(() => FirstCol(parent, layout)),
                        col: _ => FirstCol(parent, layout),
                        stack: _ => FirstCol(parent, layout),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            FirstCol(parent, layout) + layout.Relations.Slot(box).Position.ValueUnsafe().Col - 1
                    )
                )
                .GetOrElse(1);
    }
}