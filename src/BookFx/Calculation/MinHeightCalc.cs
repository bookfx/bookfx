namespace BookFx.Calculation
{
    using System;
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
            box.Children.Map(MinHeight).Compose(seed: 0, Math.Max);

        private static Sc<Cache, int> ColMinHeight(BoxCore box) =>
            box.Children.Map(MinHeight).Compose(seed: 0, (x, y) => x + y);

        private static Sc<Cache, int> StackMinHeight(BoxCore box) =>
            box.Children.Map(MinHeight).Compose(seed: 0, Math.Max);

        private static int ValueMinHeight(BoxCore box) => box.RowSpan.GetOrElse(1);

        private static int ProtoMinHeight(BoxCore box) => box.Proto.Bind(x => x.Range).ValueUnsafe().Rows;
    }
}