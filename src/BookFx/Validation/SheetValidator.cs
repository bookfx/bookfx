namespace BookFx.Validation
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Epplus.Constraint;
    using static BookFx.Functional.F;
    using static BookFx.Functional.ValidatorComposition;

    internal static class SheetValidator
    {
        public static Validator<SheetCore> Validate =>
            HarvestErrors(
                SheetName,
                BoxLocalNameUniqueness,
                PrintArea,
                Margins,
                FitToHeight,
                FitToWidth,
                Scale,
                FrozenRows,
                FrozenCols,
                AutoFilter,
                Boxes);

        public static Validator<SheetCore> SheetName =>
            sheet => sheet.Name
                .Where(name => !SheetNameRegex.IsMatch(name))
                .Map(name => Invalid<SheetCore>(Errors.Sheet.NameIsInvalid(name)))
                .GetOrElse(Valid(sheet));

        public static Validator<SheetCore> BoxLocalNameUniqueness =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.SelfAndDescendants())
                .Bind(x => x.LocalName)
                .NonUnique()
                .Traverse(name => Invalid<SheetCore>(Errors.Sheet.BoxLocalNameIsNotUnique(name)))
                .Map(_ => sheet);

        public static Validator<SheetCore> PrintArea =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.IsPrintArea)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyPrintAreas());

        public static Validator<SheetCore> FrozenRows =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.AreRowsFrozen)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyFrozenRows());

        public static Validator<SheetCore> FrozenCols =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.AreColsFrozen)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyFrozenCols());

        public static Validator<SheetCore> AutoFilter =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Bind(x => x.Descendants())
                .Where(x => x.IsAutoFilter)
                .Match(
                    empty: () => Valid(sheet),
                    one: _ => Valid(sheet),
                    more: (_, __) => Errors.Sheet.ManyAutoFilters());

        public static Validator<SheetCore> Margins =>
            sheet => sheet
                .Margins
                .AsEnumerable()
                .Map(margins => margins.ToCentimetres())
                .Bind(margins => F
                    .List(
                        margins.Top,
                        margins.Right,
                        margins.Bottom,
                        margins.Left,
                        margins.Header,
                        margins.Footer)
                    .Flatten())
                .Where(margin => margin < MinMargin || margin > MaxMargin)
                .Distinct()
                .Map(Errors.Sheet.MarginIsInvalid)
                .Match(
                    empty: () => Valid(sheet),
                    more: errors => Errors.Sheet.Aggregate(sheet, errors));

        public static Validator<SheetCore> FitToHeight =>
            sheet => sheet
                .FitToHeight
                .Where(fit => fit < MinFit || fit > MaxFit)
                .Map(scale => Invalid<SheetCore>(Errors.Sheet.FitToHeightIsInvalid(scale)))
                .GetOrElse(Valid(sheet));

        public static Validator<SheetCore> FitToWidth =>
            sheet => sheet
                .FitToWidth
                .Where(fit => fit < MinFit || fit > MaxFit)
                .Map(scale => Invalid<SheetCore>(Errors.Sheet.FitToWidthIsInvalid(scale)))
                .GetOrElse(Valid(sheet));

        public static Validator<SheetCore> Scale =>
            sheet => sheet
                .Scale
                .Where(scale => scale < MinScale || scale > MaxScale)
                .Map(scale => Invalid<SheetCore>(Errors.Sheet.ScaleIsInvalid(scale)))
                .GetOrElse(Valid(sheet));

        public static Validator<SheetCore> Boxes =>
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