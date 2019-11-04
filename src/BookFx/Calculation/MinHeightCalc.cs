namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    internal static class MinHeightCalc
    {
        public static (Result<int> Result, Cache Cache) MinHeight(BoxCore box, Cache cache) =>
            cache.GetOrEval(
                box,
                Measure.MinHight,
                () => box.Match(
                    row: _ => RowMinHeight(box, cache),
                    col: _ => ColMinHeight(box, cache),
                    stack: _ => StackMinHeight(box, cache),
                    value: _ => (ValueMinHeight(box), cache),
                    proto: _ => (ProtoMinHeight(box), cache)));

        private static (Result<int>, Cache) RowMinHeight(BoxCore box, Cache cache) =>
            box.Children.Max(
                seed: (Valid(0), cache),
                selector: MinHeight);

        private static (Result<int>, Cache) ColMinHeight(BoxCore box, Cache cache) =>
            box.Children.Sum(
                seed: (Valid(0), cache),
                selector: MinHeight);

        private static (Result<int>, Cache) StackMinHeight(BoxCore box, Cache cache) =>
            box.Children.Max(
                seed: (Valid(0), cache),
                selector: MinHeight);

        private static Result<int> ValueMinHeight(BoxCore box) =>
            box.RowSpan.GetOrElse(1);

        private static Result<int> ProtoMinHeight(BoxCore box) =>
            box.Proto.Bind(x => x.Range).ValueUnsafe().Rows;
    }
}