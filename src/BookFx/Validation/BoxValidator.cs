namespace BookFx.Validation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using static Functional.F;
    using static Functional.TeeComposition;

    internal static class BoxValidator
    {
        public static Tee<BoxCore> Validate =>
            HarvestErrors(
                RowSpanSize,
                ColSpanSize,
                RowSizeRange,
                ColSizeRange,
                Style);

        public static Tee<BoxCore> RowSpanSize =>
            box => box.RowSpan
                .Where(size => size < 1 || size > Constraint.MaxRow)
                .Match(
                    none: () => Valid(box),
                    some: size => Errors.Box.RowSpanIsInvalid(size));

        public static Tee<BoxCore> ColSpanSize =>
            box => box.ColSpan
                .Where(size => size < 1 || size > Constraint.MaxColumn)
                .Match(
                    none: () => Valid(box),
                    some: size => Errors.Box.ColSpanIsInvalid(size));

        public static Tee<BoxCore> RowSizeRange =>
            box => box.RowSizes.Values()
                .Where(size => size < Constraint.MinRowSize || size > Constraint.MaxRowSize)
                .Match(
                    empty: () => Valid(box),
                    one: size => Errors.Box.RowSizesAreInvalid(size),
                    more: (size, others) => Errors.Box.RowSizesAreInvalid(size, others));

        public static Tee<BoxCore> ColSizeRange =>
            box => box.ColSizes.Values()
                .Where(size => size < Constraint.MinColSize || size > Constraint.MaxColSize)
                .Match(
                    empty: () => Valid(box),
                    one: size => Errors.Box.ColSizesAreInvalid(size),
                    more: (size, others) => Errors.Box.ColSizesAreInvalid(size, others));

        public static Tee<BoxCore> Style =>
            box => box.Style
                .Map(StyleValidator.Validate.Invoke)
                .Match(
                    none: () => Valid(box),
                    some: result => result.Match(
                        invalid: errors => Errors.Box.Aggregate(box, errors),
                        valid: _ => Valid(box)));
    }
}