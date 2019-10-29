namespace BookFx.Calculation
{
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;

    internal static class MinDimensionCalc
    {
        public static BoxCore WithMinDimension(this BoxCore box)
        {
            var children = box.Children.Map(x => x.WithMinDimension()).ToImmutableList();

            return box.Match(
                row: x => x.RowWithMinDimension(children),
                col: x => x.ColWithMinDimension(children),
                stack: x => x.StackWithMinDimension(children),
                value: x => x.ValueWithMinDimension(),
                proto: x => x.ProtoWithMinDimension());
        }

        private static BoxCore RowWithMinDimension(this BoxCore box, ImmutableList<BoxCore> children) =>
            box.With(
                children: children,
                minDimension: Dimension.Of(
                    height: children.Map(x => x.MinDimension.Height).DefaultIfEmpty(0).Max(),
                    width: children.Sum(x => x.MinDimension.Width)));

        private static BoxCore ColWithMinDimension(this BoxCore box, ImmutableList<BoxCore> children) =>
            box.With(
                children: children,
                minDimension: Dimension.Of(
                    height: children.Sum(x => x.MinDimension.Height),
                    width: children.Map(x => x.MinDimension.Width).DefaultIfEmpty(0).Max()));

        private static BoxCore StackWithMinDimension(this BoxCore box, ImmutableList<BoxCore> children) =>
            box.With(
                children: children,
                minDimension: Dimension.Of(
                    height: children.Map(x => x.MinDimension.Height).DefaultIfEmpty(0).Max(),
                    width: children.Map(x => x.MinDimension.Width).DefaultIfEmpty(0).Max()));

        private static BoxCore ValueWithMinDimension(this BoxCore box) =>
            box.With(
                minDimension: Dimension.Of(
                    height: box.RowSpan.GetOrElse(1),
                    width: box.ColSpan.GetOrElse(1)));

        private static BoxCore ProtoWithMinDimension(this BoxCore box) =>
            box.With(
                slots: box.Slots.Map(x => x.With(box: x.Box.WithMinDimension())),
                minDimension: box.Proto.Bind(x => x.Range).ValueUnsafe().GetDimension());
    }
}