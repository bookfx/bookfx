namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class MinHeightCalc
    {
        public static Sc<Cache, int> MinHeight(BoxCore box) =>
            cache =>
                cache.GetOrEval(
                    box,
                    Measure.MinHight,
                    () => box.Match(
                        row: _ => RowMinHeight(box)(cache),
                        col: _ => ColMinHeight(box)(cache),
                        stack: _ => StackMinHeight(box)(cache),
                        value: _ => (ValueMinHeight(box), cache),
                        proto: _ => (ProtoMinHeight(box), cache)));

        private static Sc<Cache, int> RowMinHeight(BoxCore box) =>
            cache =>
                box.Children.Max(
                    seed: (0, cache),
                    selector: MinHeight);

        private static Sc<Cache, int> ColMinHeight(BoxCore box) =>
            cache =>
                box.Children.Sum(
                    seed: (0, cache),
                    selector: MinHeight);

        private static Sc<Cache, int> StackMinHeight(BoxCore box) =>
            cache =>
                box.Children.Max(
                    seed: (0, cache),
                    selector: MinHeight);

        private static int ValueMinHeight(BoxCore box) => box.RowSpan.GetOrElse(1);

        private static int ProtoMinHeight(BoxCore box) => box.Proto.Bind(x => x.Range).ValueUnsafe().Rows;
    }
}