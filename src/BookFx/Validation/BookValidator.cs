namespace BookFx.Validation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using static BookFx.Functional.ValidatorComposition;

    internal static class BookValidator
    {
        public static Validator<BookCore> Validate =>
            HarvestErrors(
                SheetNameUniqueness,
                BoxGlobalNameUniqueness,
                Sheets);

        public static Validator<BookCore> SheetNameUniqueness =>
            book => book.Sheets
                .Bind(x => x.Name)
                .NonUnique()
                .Traverse(name => Invalid<BookCore>(Errors.Book.SheetNameIsNotUnique(name)))
                .Map(_ => book);

        public static Validator<BookCore> BoxGlobalNameUniqueness =>
            book => book.Sheets
                .Bind(x => x.Box)
                .Bind(x => x.SelfAndDescendants())
                .Bind(x => x.GlobalName)
                .NonUnique()
                .Traverse(name => Invalid<BookCore>(Errors.Book.BoxGlobalNameIsNotUnique(name)))
                .Map(_ => book);

        public static Validator<BookCore> Sheets =>
            book => book.Sheets
                .Traverse(SheetValidator.Validate.Invoke)
                .Map(_ => book);
    }
}