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
        /// <summary>
        /// The empty book.
        /// </summary>
        public static readonly Book Empty = BookCore.Empty;

        private Book(BookCore core) => Get = core;

        /// <summary>
        /// Gets properties of the book.
        /// </summary>
        public BookCore Get { get; }

        [Pure]
        public static implicit operator Book(BookCore core) => new Book(core);

        /// <summary>
        /// Add sheet(s) to the book.
        /// </summary>
        /// <param name="sheet">The first sheet.</param>
        /// <param name="sheets">Other sheets.</param>
        [Pure]
        public Book Add(Sheet sheet, params Sheet[] sheets) =>
            Add(sheets.Prepend(sheet));

        /// <summary>
        /// Add sheet(s) to the book.
        /// </summary>
        [Pure]
        public Book Add(IEnumerable<Sheet> sheets) => Get.Add(sheets.Map(x => x.Get));
    }
}