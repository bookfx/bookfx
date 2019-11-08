namespace BookFx.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class WidthCalc
    {
        public static int Width(this BoxCore box, Layout layout) =>
            layout.Cache.Width(
                box,
                () => box.ShouldGrowWidth(layout)
                    ? layout
                        .Relations
                        .Parent(box)
                        .Map(parent => parent.Match(
                            row: _ =>
                                parent.Width(layout) - parent.Children.Where(x => x != box).Sum(x => x.Width(layout)),
                            col: _ => parent.Width(layout),
                            stack: _ => parent.Width(layout),
                            value: _ => throw new ArgumentException(),
                            proto: _ => throw new ArgumentException()))
                        .ValueUnsafe()
                    : box.MinWidth(layout));
    }
}