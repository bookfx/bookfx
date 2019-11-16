namespace BookFx.Cores
{
    using BookFx.Functional;
    using JetBrains.Annotations;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    /// <summary>
    /// Gets a proto properties.
    /// </summary>
    [PublicAPI]
    public sealed class ProtoCore
    {
        private ProtoCore(byte[] book, Reference reference, Option<ExcelRangeBase> range)
        {
            Book = book;
            Reference = reference;
            Range = range;
        }

        /// <summary>
        /// Gets bytes of workbook with box prototype.
        /// </summary>
        public byte[] Book { get; }

        /// <summary>
        /// Gets the sheet reference to the prototype.
        /// </summary>
        public Reference Reference { get; }

        internal Option<ExcelRangeBase> Range { get; }

        [Pure]
        internal static ProtoCore Create(byte[] book, Reference reference) =>
            new ProtoCore(book: book, reference: reference, range: None);

        [Pure]
        internal ProtoCore With(
            byte[]? book = null,
            Reference? reference = null,
            Option<ExcelRangeBase>? range = null) =>
            new ProtoCore(
                book ?? Book,
                reference ?? Reference,
                range ?? Range);
    }
}