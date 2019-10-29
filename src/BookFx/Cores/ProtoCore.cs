namespace BookFx.Cores
{
    using BookFx.Functional;
    using JetBrains.Annotations;
    using OfficeOpenXml;

    public sealed class ProtoCore
    {
        private ProtoCore(byte[] book, Reference reference, Option<ExcelRangeBase> range)
        {
            Book = book;
            Reference = reference;
            Range = range;
        }

        public byte[] Book { get; }

        public Reference Reference { get; }

        internal Option<ExcelRangeBase> Range { get; }

        [Pure]
        internal static ProtoCore Create(byte[] book, Reference reference) =>
            new ProtoCore(book: book, reference: reference, range: F.None);

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