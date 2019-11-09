namespace BookFx.Validation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Epplus.Constraint;
    using static Functional.F;
    using static Functional.TeeComposition;

    internal static class SheetValidator
    {
        public static Tee<SheetCore> Validate =>
            FailFast(
                HarvestErrors(
                    SheetName,
                    PrintArea,
                    FitToHeight,
                    FitToWidth,
                    Scale,
                    FrozenRows,
                    FrozenCols,
                    Boxes),
                HarvestErrors(
                    RootBoxWidth,
                    RootBoxHeight));

        public static Tee<SheetCore> SheetName =>
            sheet => sheet.Name
                .Where(name => !SheetNameRegex.IsMatch(name))
                .Map(name => Invalid<SheetCore>(Errors.Sheet.NameIsInvalid(name)))
                .GetOrElse(Valid(sheet));

        public static Tee<SheetCore> PrintArea =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.IsPrintArea)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyPrintAreas());

        public static Tee<SheetCore> FrozenRows =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.IsRowsFrozen)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyFrozenRows());

        public static Tee<SheetCore> FrozenCols =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.IsColsFrozen)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyFrozenCols());

        public static Tee<SheetCore> FitToHeight =>
            sheet => sheet
                .FitToHeight
                .Where(fit => fit < MinFit || fit > MaxFit)
                .Map(scale => Invalid<SheetCore>(Errors.Sheet.FitToHeightIsInvalid(scale)))
                .GetOrElse(Valid(sheet));

        public static Tee<SheetCore> FitToWidth =>
            sheet => sheet
                .FitToWidth
                .Where(fit => fit < MinFit || fit > MaxFit)
                .Map(scale => Invalid<SheetCore>(Errors.Sheet.FitToWidthIsInvalid(scale)))
                .GetOrElse(Valid(sheet));

        public static Tee<SheetCore> Scale =>
            sheet => sheet
                .Scale
                .Where(scale => scale < MinScale || scale > MaxScale)
                .Map(scale => Invalid<SheetCore>(Errors.Sheet.ScaleIsInvalid(scale)))
                .GetOrElse(Valid(sheet));

        public static Tee<SheetCore> Boxes =>
            sheet => sheet.Box
                .AsEnumerable()
                .Bind(BoxCoreExt.SelfAndDescendants)
                .Map(BoxValidator.Validate.Invoke)
                .HarvestErrors()
                .Match(
                    invalid: errors => Errors.Sheet.Aggregate(sheet, errors),
                    valid: _ => Valid(sheet));

        public static Tee<SheetCore> RootBoxWidth =>
            sheet => sheet.Box
                .Map(box => box.Placement.Dimension.Width)
                .Where(width => width > MaxColumn)
                .Map(width => Invalid<SheetCore>(Errors.Sheet.RootBoxWidthTooBig(width)))
                .GetOrElse(Valid(sheet));

        public static Tee<SheetCore> RootBoxHeight =>
            sheet => sheet.Box
                .Map(box => box.Placement.Dimension.Height)
                .Where(height => height > MaxRow)
                .Map(height => Invalid<SheetCore>(Errors.Sheet.RootBoxHeightTooBig(height)))
                .GetOrElse(Valid(sheet));
    }
}