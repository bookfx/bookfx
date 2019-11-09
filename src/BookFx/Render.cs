namespace BookFx
{
    using System.Collections.Generic;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using BookFx.Renders;
    using BookFx.Validation;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    [PublicAPI]
    public static class Render
    {
        /// <summary>
        /// Renders a <see cref="book"/> to XLSX Office Open XML format
        /// using <see cref="http://github.com/JanKallman/EPPlus">EPPlus</see>.
        /// </summary>
        /// <param name="book">A <see cref="Book"/>.</param>
        [Pure]
        public static byte[] ToBytes(this Book book) =>
            BookValidator
                .Validate(book.Get)
                .Bind(ProtoCalc.UsingBank(Generate))
                .GetOrElse(Throw);

        [Pure]
        private static Result<byte[]> Generate(BookCore book, ProtoBank bank) =>
            Calculate(book, bank)
                .Map(BookRender.Render)
                .Bind(Packer.Pack);

        [Pure]
        private static Result<BookCore> Calculate(BookCore book, ProtoBank bank) =>
            Valid(book)
                .Bind(bank.PlugProtos)
                .Map(PlacementCalc.Place)
                .Bind(PlacementValidator.Validate.Invoke)
                .Bind(SheetListCalc.AddSheetIfEmpty)
                .Bind(SheetListCalc.NameSheetsIfUnnamed);

        [Pure]
        private static byte[] Throw(IEnumerable<Error> errors) =>
            throw new InvalidBookException(Errors.Book.Aggregate(errors));
    }
}