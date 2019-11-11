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
            HarvestErrors(
                SheetName,
                PrintArea,
                FitToHeight,
                FitToWidth,
                Scale,
                FrozenRows,
                FrozenCols,
                AutoFilter,
                Boxes);

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
                .Where(x => x.AreRowsFrozen)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyFrozenRows());

        public static Tee<SheetCore> FrozenCols =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.AreColsFrozen)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyFrozenCols());

        public static Tee<SheetCore> AutoFilter =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.IsAutoFilter)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyAutoFilters());

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
    }
}