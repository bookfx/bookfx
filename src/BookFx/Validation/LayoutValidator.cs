namespace BookFx.Validation
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using static BookFx.Functional.F;

    internal static class LayoutValidator
    {
        public static Tee<BookCore> Validate =>
            book => book
                .Sheets
                .Traverse(Sheet.Invoke)
                .Map(_ => book);

        public static Tee<SheetCore> Sheet =>
            sheet => sheet
                .Box
                .AsEnumerable()
                .Traverse(RootBox.Invoke)
                .Match(
                    invalid: errors => Errors.Sheet.Aggregate(sheet, errors),
                    valid: _ => Valid(sheet));

        public static Tee<BoxCore> RootBox =>
            box => box
                .SelfAndDescendants()
                .Map(Position.Invoke)
                .HarvestErrors()
                .Map(_ => box);

        public static Tee<BoxCore> Position =>
            box =>
                box.Placement.Position.Row < 1 ||
                box.Placement.Position.Row > Constraint.MaxRow ||
                box.Placement.Position.Col < 1 ||
                box.Placement.Position.Col > Constraint.MaxColumn
                    ? Errors.Position.IsInvalid(box.Placement.Position.Row, box.Placement.Position.Col)
                    : Valid(box);
    }
}