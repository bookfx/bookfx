namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class MinWidthCalc
    {
        public static Sc<Cache, int> MinWidth(BoxCore box) =>
            cache =>
                cache.GetOrEval(
                    box,
                    Measure.MinWidth,
                    () => box.Match(
                        row: _ => RowMinWidth(box)(cache),
                        col: _ => ColMinWidth(box)(cache),
                        stack: _ => StackMinWidth(box)(cache),
                        value: _ => (ValueMinWidth(box), cache),
                        proto: _ => (ProtoMinWidth(box), cache)));

        private static Sc<Cache, int> RowMinWidth(BoxCore box) =>
            cache =>
                box.Children.Sum(
                    seed: (0, cache),
                    selector: MinWidth);

        private static Sc<Cache, int> ColMinWidth(BoxCore box) =>
            cache =>
                box.Children.Max(
                    seed: (0, cache),
                    selector: MinWidth);

        private static Sc<Cache, int> StackMinWidth(BoxCore box) =>
            cache =>
                box.Children.Max(
                    seed: (0, cache),
                    selector: MinWidth);

        private static int ValueMinWidth(BoxCore box) => box.ColSpan.GetOrElse(1);

        private static int ProtoMinWidth(BoxCore box) => box.Proto.Bind(x => x.Range).ValueUnsafe().Columns;
    }
}