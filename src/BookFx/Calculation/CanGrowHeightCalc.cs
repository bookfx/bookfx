namespace BookFx.Calculation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class CanGrowHeightCalc
    {
        public static bool CanGrowHeight(this BoxCore box, Layout layout) =>
            layout.Cache.CanGrowHeight(
                box,
                () => AutoSpan(box, layout) && box.Match(
                    row: _ => DependingOnChildren(box, layout),
                    col: _ => DependingOnChildren(box, layout),
                    stack: _ => DependingOnChildren(box, layout),
                    value: _ => box.RowSpan.IsNone,
                    proto: _ => false));

        private static bool DependingOnChildren(BoxCore box, Layout layout) =>
            box.Children.Any(child => child.CanGrowHeight(layout));

        private static bool AutoSpan(BoxCore box, Layout layout) =>
            box
                .RowAutoSpan
                .OrElse(layout
                    .Relations
                    .Parent(box)
                    .Map(parent => AutoSpan(parent, layout)))
                .GetOrElse(true);
    }
}