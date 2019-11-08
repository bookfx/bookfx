namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class WidthCalc
    {
        public static int Width(this BoxCore box, Layout layout) =>
            layout.Cache.GetOrCompute(
                key: (box, Measure.Width),
                f: () => box.Match(
                    row: _ => OfComposite(box, layout),
                    col: _ => OfComposite(box, layout),
                    stack: _ => OfComposite(box, layout),
                    value: _ => OfValue(box, layout),
                    proto: _ => OfComposite(box, layout)));

        private static int OfComposite(BoxCore box, Layout layout) => box.MinWidth(layout.Cache);

        private static int OfValue(BoxCore box, Layout layout) =>
            box
                .ColSpan
                .OrElse(() => layout
                    .Relations
                    .Parent(box)
                    .Map(
                        row: _ => 1,
                        col: parent => parent.MinWidth(layout.Cache),
                        stack: parent => parent.MinWidth(layout.Cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => 1))
                .GetOrElse(1);
    }
}