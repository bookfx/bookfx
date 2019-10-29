namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class ProtoCalc
    {
        public static Func<BookCore, T> UsingBank<T>(Func<BookCore, ProtoBank, T> f) =>
            book => Using(new ProtoBank(book.ProtoBooks()), f.Apply(book));

        public static Result<BookCore> PlugProtos(this ProtoBank bank, BookCore book) =>
            book.Sheets.Traverse(bank.PlugProtos).Map(book.WithSheets);

        public static Result<SheetCore> PlugProtos(this ProtoBank bank, SheetCore sheet) =>
            Valid(Fun((Option<BoxCore> box, Option<ExcelWorksheet> protoSheet) =>
                    sheet.With(box: box, protoSheet: protoSheet)))
                .Apply(sheet.Box.Traverse(bank.PlugProtos))
                .Apply(sheet.ProtoBook.Traverse(book => bank.GetSheet(book, sheet.ProtoName)));

        public static Result<BoxCore> PlugProtos(this ProtoBank bank, BoxCore box) =>
            box.Match(
                row: x => x.CompositeBoxWithProtos(bank),
                col: x => x.CompositeBoxWithProtos(bank),
                stack: x => x.CompositeBoxWithProtos(bank),
                value: x => x,
                proto: x => x.ProtoBoxWithProtos(bank));

        private static Result<BoxCore> CompositeBoxWithProtos(this BoxCore box, ProtoBank bank) =>
            box.Children.Traverse(bank.PlugProtos).Map(children => box.With(children: children));

        private static Result<BoxCore> ProtoBoxWithProtos(this BoxCore box, ProtoBank bank) =>
            from newProto in box.Proto.ToResultUnsafe().Bind(bank.PlugRange)
            from newSlots in box.Slots.Traverse(slot =>
                Valid(Fun((BoxCore slotBox, Position slotPosition) => slot.With(box: slotBox, position: slotPosition)))
                    .Apply(bank.PlugProtos(slot.Box))
                    .Apply(bank.GetPosition(newProto.Book, baseRef: newProto.Reference, slotRef: slot.Reference)))
            select box.With(proto: newProto, slots: newSlots);

        private static Result<ProtoCore> PlugRange(this ProtoBank bank, ProtoCore proto) =>
            Valid(Fun((ExcelRangeBase range) => proto.With(range: range)))
                .Apply(bank.GetRange(proto.Book, proto.Reference));
    }
}