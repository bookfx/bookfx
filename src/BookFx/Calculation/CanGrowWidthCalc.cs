namespace BookFx.Calculation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class CanGrowWidthCalc
    {
        public static bool CanGrowWidth(this BoxCore box, Layout layout) =>
            layout.Cache.CanGrowWidth(
                box,
                () => AutoSpan(box, layout) && box.Match(
                    row: _ => DependingOnChildren(box, layout),
                    col: _ => DependingOnChildren(box, layout),
                    stack: _ => DependingOnChildren(box, layout),
                    value: _ => box.ColSpan.IsNone,
                    proto: _ => false));

        private static bool DependingOnChildren(BoxCore box, Layout layout) =>
            box.Children.Any(child => child.CanGrowWidth(layout));

        private static bool AutoSpan(BoxCore box, Layout layout) =>
            box
                .ColAutoSpan
                .OrElse(layout
                    .Relations
                    .Parent(box)
                    .Map(parent => AutoSpan(parent, layout)))
                .GetOrElse(false);
    }
}