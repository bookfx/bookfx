namespace BookFx.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class ShouldGrowWidthCalc
    {
        public static bool ShouldGrowWidth(this BoxCore box, Layout layout) =>
            layout.Cache.ShouldGrowWidth(
                box,
                () =>
                    box.CanGrowWidth(layout) &&
                    layout
                        .Relations
                        .Parent(box)
                        .Map(parent => parent.Match(
                            row: _ =>
                                parent.ShouldGrowWidth(layout) &&
                                parent.Children.SkipWhile(x => x != box).Skip(1).All(x => !x.CanGrowWidth(layout)),
                            col: _ =>
                                parent.ShouldGrowWidth(layout) || box.MinWidth(layout) < parent.MinWidth(layout),
                            stack: _ =>
                                parent.ShouldGrowWidth(layout) || box.MinWidth(layout) < parent.MinWidth(layout),
                            value: _ => throw new InvalidOperationException(),
                            proto: _ => false))
                        .GetOrElse(false));
    }
}