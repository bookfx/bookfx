namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static MinHeightCalc;

    internal static class HeightCalc
    {
        public static int Height(BoxCore box, Layout layout) =>
            layout.Cache.GetOrCompute(
                key: (box, Measure.Hight),
                f: () => box.Match(
                    row: _ => OfComposite(box, layout),
                    col: _ => OfComposite(box, layout),
                    stack: _ => OfComposite(box, layout),
                    value: _ => OfValue(box, layout),
                    proto: _ => OfComposite(box, layout)));

        private static int OfComposite(BoxCore box, Layout layout) => MinHeight(box, layout.Cache);

        private static int OfValue(BoxCore box, Layout layout) =>
            box
                .RowSpan
                .OrElse(() => layout
                    .Relations
                    .Parent(box)
                    .Map(
                        row: parent => MinHeight(parent, layout.Cache),
                        col: _ => 1,
                        stack: parent => MinHeight(parent, layout.Cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => 1))
                .GetOrElse(1);
    }
}