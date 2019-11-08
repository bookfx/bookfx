namespace BookFx.Calculation
{
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
                        .Map(parent =>
                            parent.ShouldGrowWidth(layout) &&
                            parent.Children.SkipWhile(x => x != box).Skip(1).Any(x => x.CanGrowWidth(layout)))
                        .GetOrElse(false));
    }
}