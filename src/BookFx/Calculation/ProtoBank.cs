namespace BookFx.Calculation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IO;
    using BookFx.Epplus;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal sealed class ProtoBank : IDisposable
    {
        private readonly ImmutableDictionary<byte[], ExcelPackage> _packages;

        public ProtoBank(IEnumerable<byte[]> books) =>
            _packages = books.ToImmutableDictionary(
                book => book,
                book => Using(new MemoryStream(book), stream => new ExcelPackage(stream)));

        [Pure]
        public Result<ExcelRangeBase> GetRange(byte[] book, Reference rangeRef)
        {
            var names = _packages[book].Workbook.Names;
            var name = rangeRef.ToString();

            if (names.ContainsKey(name))
            {
                return names[name];
            }

            return Errors.Excel.ProtoRefNotFound(rangeRef);
        }

        [Pure]
        public Result<Position> GetPosition(byte[] book, Reference rangeRef) =>
            GetRange(book, rangeRef).Bind(range => range.GetPosition());

        [Pure]
        public Result<Position> GetPosition(byte[] book, Reference baseRef, Reference slotRef) =>
            from basePosition in GetPosition(book, baseRef)
            from slotPosition in GetPosition(book, slotRef)
            from relatingSlotPosition in slotPosition.RelatingTo(basePosition)
            select relatingSlotPosition;

        [Pure]
        public Result<ExcelWorksheet> GetSheet(byte[] book, Option<string> sheetName)
        {
            return sheetName.Match(
                none: GetOne,
                some: GetByName
            );

            Result<ExcelWorksheet> GetOne() =>
                _packages[book]
                    .Workbook
                    .Worksheets
                    .Match(
                        empty: () => throw new InvalidOperationException(),
                        one: Valid,
                        more: (x, xs) => Errors.Excel.SheetProtoNameShouldBeSpecified()
                    );

            Result<ExcelWorksheet> GetByName(string name) =>
                _packages[book]
                    .Workbook
                    .Worksheets
                    .Find(x => x.Name == name)
                    .ToResult(() => Errors.Excel.SheetProtoNameNotFound(name));
        }

        public void Dispose() => _packages.Values.ForEach(package => package.Dispose());
    }
}