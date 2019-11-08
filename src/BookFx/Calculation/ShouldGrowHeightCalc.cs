namespace BookFx.Calculation
{
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
                        .Map(parent =>
                            parent.ShouldGrowHeight(layout) &&
                            parent.Children.SkipWhile(x => x != box).Skip(1).Any(x => x.CanGrowHeight(layout)))
                        .GetOrElse(false));
    }
}