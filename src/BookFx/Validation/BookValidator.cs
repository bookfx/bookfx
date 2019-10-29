namespace BookFx.Validation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using static BookFx.Functional.TeeComposition;

    internal static class BookValidator
    {
        public static Tee<BookCore> Validate =>
            HarvestErrors(
                SheetNameUniqueness,
                BoxNameUniqueness,
                Sheets);

        public static Tee<BookCore> SheetNameUniqueness =>
            book => book.Sheets
                .Bind(x => x.Name)
                .NonUnique()
                .Traverse(name => Invalid<BookCore>(Errors.Book.SheetNameIsNotUnique(name)))
                .Map(_ => book);

        public static Tee<BookCore> BoxNameUniqueness =>
            book => book.Sheets
                .Bind(x => x.Box)
                .Bind(x => x.SelfAndDescendants())
                .Bind(x => x.Name)
                .NonUnique()
                .Traverse(name => Invalid<BookCore>(Errors.Book.BoxNameIsNotUnique(name)))
                .Map(_ => book);

        public static Tee<BookCore> Sheets =>
            book => book.Sheets
                .Traverse(SheetValidator.Validate.Invoke)
                .Map(_ => book);
    }
}