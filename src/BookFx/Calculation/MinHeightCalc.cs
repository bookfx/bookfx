namespace BookFx.Calculation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class MinHeightCalc
    {
        public static int MinHeight(this BoxCore box, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.MinHight),
                f: () => box.Match(
                    row: _ => OfRow(box, cache),
                    col: _ => OfCol(box, cache),
                    stack: _ => OfStack(box, cache),
                    value: _ => OfValue(box),
                    proto: _ => OfProto(box)));

        private static int OfRow(BoxCore box, Cache cache) =>
            box.Children.Map(child => child.MinHeight(cache)).DefaultIfEmpty(0).Max();

        private static int OfCol(BoxCore box, Cache cache) =>
            box.Children.Map(child => child.MinHeight(cache)).Sum();

        private static int OfStack(BoxCore box, Cache cache) =>
            box.Children.Map(child => child.MinHeight(cache)).DefaultIfEmpty(0).Max();

        private static int OfValue(BoxCore box) =>
            box.RowSpan.GetOrElse(1);

        private static int OfProto(BoxCore box) =>
            box.Proto.Bind(x => x.Range).ValueUnsafe().Rows;
    }
}