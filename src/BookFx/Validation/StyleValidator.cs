namespace BookFx.Validation
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using static BookFx.Functional.TeeComposition;

    internal static class StyleValidator
    {
        public static Tee<BoxStyleCore> Validate =>
            HarvestErrors(
                FontSize,
                IndentSize,
                FontName,
                Format);

        public static Tee<BoxStyleCore> FontSize =>
            style => style.FontSize
                .Where(size => size < Constraint.MinFontSize || size > Constraint.MaxFontSize)
                .Map(size => Invalid<BoxStyleCore>(Errors.Style.FontSizeIsInvalid(size)))
                .GetOrElse(Valid(style));

        public static Tee<BoxStyleCore> IndentSize =>
            style => style.Indent
                .Where(size => size < Constraint.MinIndentSize || size > Constraint.MaxIndentSize)
                .Map(size => Invalid<BoxStyleCore>(Errors.Style.IndentSizeIsInvalid(size)))
                .GetOrElse(Valid(style));

        public static Tee<BoxStyleCore> FontName =>
            style => style.FontName
                .Where(string.IsNullOrWhiteSpace)
                .Map(_ => Invalid<BoxStyleCore>(Errors.Style.FontNameIsEmpty()))
                .GetOrElse(Valid(style));

        public static Tee<BoxStyleCore> Format =>
            style => style.Format
                .Where(string.IsNullOrWhiteSpace)
                .Map(_ => Invalid<BoxStyleCore>(Errors.Style.FormatIsEmpty()))
                .GetOrElse(Valid(style));
    }
}