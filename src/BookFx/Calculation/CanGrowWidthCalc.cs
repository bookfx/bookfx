namespace BookFx.Calculation
{
    using System.Linq;
    using BookFx.Cores;

    internal static class CanGrowWidthCalc
    {
        public static bool CanGrowWidth(this BoxCore box, Layout layout) =>
            layout.Cache.CanGrowWidth(
                box,
                () => box.Match(
                    row: _ => DependingOnChildren(box, layout),
                    col: _ => DependingOnChildren(box, layout),
                    stack: _ => DependingOnChildren(box, layout),
                    value: _ => box.ColSpan.IsNone,
                    proto: _ => false));

        private static bool DependingOnChildren(BoxCore box, Layout layout) =>
            box.Children.Any(child => child.CanGrowWidth(layout));
    }
}