namespace BookFx.Validation
{
    using System;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using static BookFx.Functional.ValidatorComposition;

    internal static class BoxValidator
    {
        public static Validator<BoxCore> Validate =>
            HarvestErrors(
                RowSpanSize,
                ColSpanSize,
                RowSizeRange,
                ColSizeRange,
                Name(box => box.GlobalName),
                Name(box => box.LocalName),
                Style);

        public static Validator<BoxCore> RowSpanSize =>
            box => box.RowSpan
                .Where(size => size < 1 || size > Constraint.MaxRow)
                .Match(
                    none: () => Valid(box),
                    some: size => Errors.Box.RowSpanIsInvalid(size));

        public static Validator<BoxCore> ColSpanSize =>
            box => box.ColSpan
                .Where(size => size < 1 || size > Constraint.MaxColumn)
                .Match(
                    none: () => Valid(box),
                    some: size => Errors.Box.ColSpanIsInvalid(size));

        public static Validator<BoxCore> RowSizeRange =>
            box => box.RowSizes.Values()
                .Where(size => size < Constraint.MinRowSize || size > Constraint.MaxRowSize)
                .Match(
                    empty: () => Valid(box),
                    one: size => Errors.Box.RowSizesAreInvalid(size),
                    more: (size, others) => Errors.Box.RowSizesAreInvalid(size, others));

        public static Validator<BoxCore> ColSizeRange =>
            box => box.ColSizes.Values()
                .Where(size => size < Constraint.MinColSize || size > Constraint.MaxColSize)
                .Match(
                    empty: () => Valid(box),
                    one: size => Errors.Box.ColSizesAreInvalid(size),
                    more: (size, others) => Errors.Box.ColSizesAreInvalid(size, others));

        public static Validator<BoxCore> Style =>
            box => box.Style
                .Map(StyleValidator.Validate.Invoke)
                .Match(
                    none: () => Valid(box),
                    some: result => result.Match(
                        invalid: errors => Errors.Box.Aggregate(box, errors),
                        valid: _ => Valid(box)));

        public static Validator<BoxCore> Name(Func<BoxCore, Option<string>> getName) =>
            box => getName(box).Match(
                none: () => Valid(box),
                some: name =>
                    Constraint.RangeNameRegex.IsMatch(name) &&
                    !Constraint.A1RangeNameRegex.IsMatch(name) &&
                    !Constraint.R1C1RangeNameRegex.IsMatch(name)
                        ? Valid(box)
                        : Errors.Box.NameIsInvalid(name));
    }
}