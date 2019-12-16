namespace BookFx.Cores
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using JetBrains.Annotations;

    internal static class BookCoreExt
    {
        [Pure]
        public static IEnumerable<byte[]> ProtoBooks(this BookCore book) =>
            book
                .Sheets
                .Bind(x => x.Box)
                .Bind(x => x.Protos())
                .Map(x => x.Book)
                .Union(book.Sheets.Bind(x => x.ProtoBook))
                .Distinct();
    }
}