namespace BookFx.Calculation
{
    using System.Linq;
    using BookFx.Cores;

    internal static class CanGrowHeightCalc
    {
        public static bool CanGrowHeight(this BoxCore box, Layout layout) =>
            layout.Cache.CanGrowHeight(
                box,
                () => box.Match(
                    row: _ => DependingOnChildren(box, layout),
                    col: _ => DependingOnChildren(box, layout),
                    stack: _ => DependingOnChildren(box, layout),
                    value: _ => box.RowSpan.IsNone,
                    proto: _ => false));

        private static bool DependingOnChildren(BoxCore box, Layout layout) =>
            box.Children.Any(child => child.CanGrowHeight(layout));
    }
}