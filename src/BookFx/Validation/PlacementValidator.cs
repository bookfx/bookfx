namespace BookFx.Validation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Epplus.Constraint;
    using static BookFx.Functional.F;
    using static BookFx.Functional.ValidatorComposition;

    internal static class PlacementValidator
    {
        public static Validator<BookCore> Validate =>
            book => book
                .Sheets
                .Traverse(Sheet.Invoke)
                .Map(_ => book);

        public static Validator<SheetCore> Sheet =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Traverse(RootBox.Invoke)
                .Match(
                    invalid: errors => Errors.Sheet.Aggregate(sheet, errors),
                    valid: _ => Valid(sheet));

        public static Validator<BoxCore> RootBox =>
            HarvestErrors(
                RootBoxWidth,
                RootBoxHeight,
                AllBoxes);

        public static Validator<BoxCore> RootBoxWidth =>
            box => box.Placement.Dimension.Width > MaxColumn
                ? Invalid<BoxCore>(Errors.Placement.RootBoxWidthTooBig(box.Placement.Dimension.Width))
                : Valid(box);

        public static Validator<BoxCore> RootBoxHeight =>
            box => box.Placement.Dimension.Height > MaxRow
                ? Invalid<BoxCore>(Errors.Placement.RootBoxHeightTooBig(box.Placement.Dimension.Height))
                : Valid(box);

        public static Validator<BoxCore> AllBoxes =>
            box => box
                .SelfAndDescendants()
                .Map(Position.Invoke)
                .HarvestErrors()
                .Map(_ => box);

        public static Validator<BoxCore> Position =>
            box =>
                box.Placement.Position.Row.IsBetween(1, MaxRow) &&
                box.Placement.Position.Col.IsBetween(1, MaxColumn)
                    ? Valid(box)
                    : Errors.Placement.IsInvalid(box.Placement.Position.Row, box.Placement.Position.Col);

        private static bool IsBetween(this int @this, int from, int to) => @this >= from && @this <= to;
    }
}