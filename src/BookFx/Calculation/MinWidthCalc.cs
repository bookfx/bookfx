namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        int
    >;

    internal static class MinWidthCalc
    {
        public static (int Result, Cache Cache) MinWidth(BoxCore box, Cache cache) =>
            cache.GetOrEval(
                box,
                Measure.MinWidth,
                () => box.Match(
                    row: _ => RowMinWidth(box, cache),
                    col: _ => ColMinWidth(box, cache),
                    stack: _ => StackMinWidth(box, cache),
                    value: _ => (ValueMinWidth(box), cache),
                    proto: _ => (ProtoMinWidth(box), cache)));

        private static (int, Cache) RowMinWidth(BoxCore box, Cache cache) =>
            box.Children.Sum(
                seed: (0, cache),
                selector: MinWidth);

        private static (int, Cache) ColMinWidth(BoxCore box, Cache cache) =>
            box.Children.Max(
                seed: (0, cache),
                selector: MinWidth);

        private static (int, Cache) StackMinWidth(BoxCore box, Cache cache) =>
            box.Children.Max(
                seed: (0, cache),
                selector: MinWidth);

        private static int ValueMinWidth(BoxCore box) =>
            box.ColSpan.GetOrElse(1);

        private static int ProtoMinWidth(BoxCore box) =>
            box.Proto.Bind(x => x.Range).ValueUnsafe().Columns;
    }
}