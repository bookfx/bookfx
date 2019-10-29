namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;

    /// <summary>
    /// Book is a class to describe workbook.
    /// </summary>
    [PublicAPI]
    public sealed class Book
    {
        public static readonly Book Empty = BookCore.Empty;

        private Book(BookCore core) => Get = core;

        public BookCore Get { get; }

        [Pure]
        public static implicit operator Book(BookCore core) => new Book(core);

        [Pure]
        public Book Add(Sheet sheet, params Sheet[] sheets) =>
            Add(sheets.Prepend(sheet));

        [Pure]
        public Book Add(IEnumerable<Sheet> sheets) => Get.Add(sheets.Map(x => x.Get));
    }
}