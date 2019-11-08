namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static MinWidthCalc;

    internal static class WidthCalc
    {
        public static int Width(BoxCore box, Layout layout) =>
            layout.Cache.GetOrCompute(
                key: (box, Measure.Width),
                f: () => box.Match(
                    row: _ => OfComposite(box, layout),
                    col: _ => OfComposite(box, layout),
                    stack: _ => OfComposite(box, layout),
                    value: _ => OfValue(box, layout),
                    proto: _ => OfComposite(box, layout)));

        private static int OfComposite(BoxCore box, Layout layout) => MinWidth(box, layout.Cache);

        private static int OfValue(BoxCore box, Layout layout) =>
            box
                .ColSpan
                .OrElse(() => layout
                    .Relations
                    .Parent(box)
                    .Map(
                        row: _ => 1,
                        col: parent => MinWidth(parent, layout.Cache),
                        stack: parent => MinWidth(parent, layout.Cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => 1))
                .GetOrElse(1);
    }
}