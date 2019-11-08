namespace BookFx.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class HeightCalc
    {
        public static int Height(this BoxCore box, Layout layout) =>
            layout.Cache.Height(
                box,
                () => box.ShouldGrowHeight(layout)
                    ? layout
                        .Relations
                        .Parent(box)
                        .Map(parent => parent.Match(
                            row: _ => parent.Height(layout),
                            col: _ =>
                                parent.Height(layout) - parent.Children.Where(x => x != box).Sum(x => x.Height(layout)),
                            stack: _ => parent.Height(layout),
                            value: _ => throw new ArgumentException(),
                            proto: _ => throw new ArgumentException()))
                        .ValueUnsafe()
                    : box.MinHeight(layout));
    }
}