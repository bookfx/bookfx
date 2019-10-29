namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class LayoutCalc
    {
        public static Result<BookCore> LayOut(this BookCore book) =>
            book.Sheets.Traverse(LayOut).Map(book.WithSheets);

        public static Result<SheetCore> LayOut(this SheetCore sheet) =>
            sheet.Box.Traverse(LayOut).Map(sheet.WithBox);

        public static Result<BoxCore> LayOut(this BoxCore box) => box.WithMinDimension().WithPlacement();

        public static SheetCore LayOutUnsafe(this SheetCore sheet) =>
            sheet.LayOut().ValueUnsafe();

        public static BoxCore LayOutUnsafe(this BoxCore box) => box.LayOut().ValueUnsafe();
    }
}