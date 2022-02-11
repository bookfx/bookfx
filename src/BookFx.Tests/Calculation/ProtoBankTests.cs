namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using BookFx.Functional;
    using FluentAssertions;
    using OfficeOpenXml;
    using Xunit;
    using static BookFx.Functional.F;

    public class ProtoBankTests
    {
        [Fact]
        public void GetRange_NameExists_Expected()
        {
            const string theRef = "Ref";
            const string theValue = "Value";
            var protoBook = Make.Value(theValue).NameGlobally(theRef).ToSheet().ToBook().ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetRange(protoBook, theRef);

            result.Map(x => x.Value).Should().Be(Valid<object>(theValue));
        }

        [Fact]
        public void GetRange_NameNotExists_Invalid()
        {
            var protoBook = Make.Book().ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetRange(protoBook, "Unknown");

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GetPosition_NameExists_Expected()
        {
            const string theRef = "Ref";
            var protoBook = Make.Value().NameGlobally(theRef).ToSheet().ToBook().ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetPosition(protoBook, theRef);

            result.Should().Be(Valid(Position.Initial));
        }

        [Fact]
        public void GetPosition_NameNotExists_Invalid()
        {
            var protoBook = Make.Book().ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetPosition(protoBook, "Unknown");

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GetSheet_1SheetAndNameNotSpecified_Expected()
        {
            const string theValue = "Value";
            var protoBook = Make.Value(theValue).ToSheet().ToBook().ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetSheet(protoBook, None);

            result.ValueUnsafe().Cells[1, 1].Value.Should().Be(theValue);
        }

        [Fact]
        public void GetSheet_2SheetsAndNameSpecified_Expected()
        {
            const string sheetName = "Sheet name";
            const string theValue = "Value";
            var protoBook = Make.Value(theValue).ToSheet().Name(sheetName).ToBook().Add(Make.Sheet()).ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetSheet(protoBook, sheetName);

            result.ValueUnsafe().Cells[1, 1].Value.Should().Be(theValue);
        }

        [Fact]
        public void GetSheet_1SheetAndWrongNameSpecified_Invalid()
        {
            const string unknownName = "Unknown name";
            var protoBook = Make.Sheet("Proto name").ToBook().ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetSheet(protoBook, unknownName);

            result.Should().Be(Invalid<ExcelWorksheet>(Errors.Excel.SheetProtoNameNotFound(unknownName)));
        }

        [Fact]
        public void GetSheet_2SheetsAndNameNotSpecified_Invalid()
        {
            var protoBook = Make.Book(Make.Sheet(), Make.Sheet()).ToBytes();
            var sut = new ProtoBank(List(protoBook));

            var result = sut.GetSheet(protoBook, None);

            result.Should().Be(Invalid<ExcelWorksheet>(Errors.Excel.SheetProtoNameShouldBeSpecified()));
        }
    }
}