namespace BookFx.Calculation
{
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;

    internal static class SheetListCalc
    {
        [Pure]
        public static Result<BookCore> AddSheetIfEmpty(this BookCore book) =>
            book.Sheets.IsEmpty ? book.Add(SheetCore.Empty) : book;

        [Pure]
        public static Result<BookCore> NameSheetsIfUnnamed(this BookCore book)
        {
            var sheets = book.Sheets;

            var indexOfUnnamed = sheets.FindIndex(x => x.Name.IsNone);

            if (indexOfUnnamed == -1)
            {
                return book;
            }

            var usedNames = sheets.Bind(x => x.Name).ToImmutableHashSet();
            var namedSheet = sheets[indexOfUnnamed].With(name: GetNewName(usedNames));
            sheets = sheets.SetItem(indexOfUnnamed, namedSheet);

            return book.With(sheets).NameSheetsIfUnnamed();
        }

        [Pure]
        public static BookCore AddSheetIfEmptyUnsafe(this BookCore book) =>
            book.AddSheetIfEmpty().ValueUnsafe();

        [Pure]
        public static BookCore NameSheetsIfUnnamedUnsafe(this BookCore book) =>
            book.NameSheetsIfUnnamed().ValueUnsafe();

        private static string GetNewName(ImmutableHashSet<string> usedNames) =>
            Enumerable.Range(1, usedNames.Count + 1)
                .Map(i => $"Sheet{i}")
                .First(name => !usedNames.Contains(name));
    }
}