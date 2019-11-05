namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        int
    >;

    internal static class MinHeightCalc
    {
        public static (int Result, Cache Cache) MinHeight(BoxCore box, Cache cache) =>
            cache.GetOrEval(
                box,
                Measure.MinHight,
                () => box.Match(
                    row: _ => RowMinHeight(box, cache),
                    col: _ => ColMinHeight(box, cache),
                    stack: _ => StackMinHeight(box, cache),
                    value: _ => (ValueMinHeight(box), cache),
                    proto: _ => (ProtoMinHeight(box), cache)));

        private static (int, Cache) RowMinHeight(BoxCore box, Cache cache) =>
            box.Children.Max(
                seed: (0, cache),
                selector: MinHeight);

        private static (int, Cache) ColMinHeight(BoxCore box, Cache cache) =>
            box.Children.Sum(
                seed: (0, cache),
                selector: MinHeight);

        private static (int, Cache) StackMinHeight(BoxCore box, Cache cache) =>
            box.Children.Max(
                seed: (0, cache),
                selector: MinHeight);

        private static int ValueMinHeight(BoxCore box) =>
            box.RowSpan.GetOrElse(1);

        private static int ProtoMinHeight(BoxCore box) =>
            box.Proto.Bind(x => x.Range).ValueUnsafe().Rows;
    }
}