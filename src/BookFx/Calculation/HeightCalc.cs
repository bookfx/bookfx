namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class HeightCalc
    {
        public static int Height(this BoxCore box, Layout layout) =>
            layout.Cache.Height(
                box,
                () => box.Match(
                    row: _ => OfComposite(box, layout),
                    col: _ => OfComposite(box, layout),
                    stack: _ => OfComposite(box, layout),
                    value: _ => OfValue(box, layout),
                    proto: _ => OfComposite(box, layout)));

        private static int OfComposite(BoxCore box, Layout layout) => box.MinHeight(layout.Cache);

        private static int OfValue(BoxCore box, Layout layout) =>
            box
                .RowSpan
                .OrElse(() => layout
                    .Relations
                    .Parent(box)
                    .Map(
                        row: parent => parent.MinHeight(layout.Cache),
                        col: _ => 1,
                        stack: parent => parent.MinHeight(layout.Cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => 1))
                .GetOrElse(1);
    }
}