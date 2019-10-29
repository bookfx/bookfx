namespace BookFx.Cores
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class BookCore
    {
        internal static readonly BookCore Empty = Create();

        private BookCore(IEnumerable<SheetCore> sheets) => Sheets = sheets.ToImmutableList();

        public ImmutableList<SheetCore> Sheets { get; }

        [Pure]
        internal static BookCore Create(IEnumerable<SheetCore>? sheets = null) =>
            new BookCore(sheets ?? ImmutableList<SheetCore>.Empty);

        [Pure]
        internal BookCore Add(SheetCore sheet) => With(sheets: Sheets.Add(sheet));

        [Pure]
        internal BookCore Add(IEnumerable<SheetCore> sheets) =>
            With(sheets: Sheets.AddRange(sheets));

        [Pure]
        internal BookCore WithSheets(IEnumerable<SheetCore> sheets) => With(sheets: sheets);

        [Pure]
        internal BookCore With(IEnumerable<SheetCore>? sheets = null) => new BookCore(sheets ?? Sheets);
    }
}