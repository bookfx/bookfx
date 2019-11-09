namespace BookFx.Calculation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class MinHeightCalc
    {
        public static int MinHeight(this BoxCore box, Layout layout) =>
            layout.Cache.MinHeight(
                box,
                () => box.Match(
                    row: _ => OfRow(box, layout),
                    col: _ => OfCol(box, layout),
                    stack: _ => OfStack(box, layout),
                    value: _ => OfValue(box),
                    proto: _ => OfProto(box)));

        private static int OfRow(BoxCore box, Layout layout) =>
            box.Children.Map(child => child.MinHeight(layout)).DefaultIfEmpty(0).Max();

        private static int OfCol(BoxCore box, Layout layout) =>
            box.Children.Map(child => child.MinHeight(layout)).Sum();

        private static int OfStack(BoxCore box, Layout layout) =>
            box.Children.Map(child => child.MinHeight(layout)).DefaultIfEmpty(0).Max();

        private static int OfValue(BoxCore box) =>
            box.RowSpan.GetOrElse(1);

        private static int OfProto(BoxCore box) =>
            box.Proto.Bind(x => x.Range).ValueUnsafe().Rows;
    }
}