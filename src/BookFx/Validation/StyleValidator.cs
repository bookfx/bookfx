namespace BookFx.Validation
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using static BookFx.Functional.ValidatorComposition;

    internal static class StyleValidator
    {
        public static Validator<BoxStyleCore> Validate =>
            HarvestErrors(
                FontSize,
                Rotation,
                IndentSize,
                FontName,
                Format);

        public static Validator<BoxStyleCore> FontSize =>
            style => style.FontSize
                .Where(size => size < Constraint.MinFontSize || size > Constraint.MaxFontSize)
                .Map(size => Invalid<BoxStyleCore>(Errors.Style.FontSizeIsInvalid(size)))
                .GetOrElse(Valid(style));

        public static Validator<BoxStyleCore> Rotation =>
            style => style.Rotation
                .Where(rotation => rotation < Constraint.MinRotation || rotation > Constraint.MaxRotation)
                .Map(rotation => Invalid<BoxStyleCore>(Errors.Style.RotationIsInvalid(rotation)))
                .GetOrElse(Valid(style));

        public static Validator<BoxStyleCore> IndentSize =>
            style => style.Indent
                .Where(size => size < Constraint.MinIndentSize || size > Constraint.MaxIndentSize)
                .Map(size => Invalid<BoxStyleCore>(Errors.Style.IndentSizeIsInvalid(size)))
                .GetOrElse(Valid(style));

        public static Validator<BoxStyleCore> FontName =>
            style => style.FontName
                .Where(string.IsNullOrWhiteSpace)
                .Map(_ => Invalid<BoxStyleCore>(Errors.Style.FontNameIsEmpty()))
                .GetOrElse(Valid(style));

        public static Validator<BoxStyleCore> Format =>
            style => style.Format
                .Where(string.IsNullOrWhiteSpace)
                .Map(_ => Invalid<BoxStyleCore>(Errors.Style.FormatIsEmpty()))
                .GetOrElse(Valid(style));
    }
}