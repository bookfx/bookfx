namespace BookFx.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class ShouldGrowHeightCalc
    {
        public static bool ShouldGrowHeight(this BoxCore box, Layout layout) =>
            layout.Cache.ShouldGrowHeight(
                box,
                () =>
                    box.CanGrowHeight(layout) &&
                    layout
                        .Relations
                        .Parent(box)
                        .Map(parent => parent.Match(
                            row: _ =>
                                parent.ShouldGrowHeight(layout) || box.MinHeight(layout) < parent.MinHeight(layout),
                            col: _ =>
                                parent.ShouldGrowHeight(layout) &&
                                parent.Children.SkipWhile(x => x != box).Skip(1).All(x => !x.CanGrowHeight(layout)),
                            stack: _ =>
                                parent.ShouldGrowHeight(layout) || box.MinHeight(layout) < parent.MinHeight(layout),
                            value: _ => throw new InvalidOperationException(),
                            proto: _ => false))
                        .GetOrElse(false));
    }
}