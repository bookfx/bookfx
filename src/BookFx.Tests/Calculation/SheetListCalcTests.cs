namespace BookFx.Tests.Calculation
{
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class SheetListCalcTests
    {
        [Fact]
        public void AddSheetIfEmpty_Empty_1Sheet() =>
            Make.Book().Get.AddSheetIfEmptyUnsafe().Sheets.Should().HaveCount(1);

        [Fact]
        public void AddSheetIfEmpty_1Sheet_1Sheet() =>
            Make.Book(Make.Sheet()).Get.AddSheetIfEmptyUnsafe().Sheets.Should().HaveCount(1);

        [Fact]
        public void NameSheetsIfUnnamed_1UnnamedSheet_BookWithSheet1() =>
            Make.Book(Make.Sheet()).Get.NameSheetsIfUnnamedUnsafe().Sheets.Single().Name.Should().Be(Some("Sheet1"));

        [Fact]
        public void NameSheetsIfUnnamed_1NamedSheet_1NamedSheet()
        {
            const string name = "The First Sheet";
            var book = Make.Book(Make.Sheet(name)).Get;

            var result = book.NameSheetsIfUnnamedUnsafe();

            result.Sheets.Single().Name.Should().Be(Some(name));
        }

        [Fact]
        public void NameSheetsIfUnnamed_FirstOf2SheetsNamedAsDefault_TwoSheets()
        {
            const string firstSheetName = "Sheet1";
            var book = Make.Book(Make.Sheet(firstSheetName), Make.Sheet()).Get;

            var result = book.NameSheetsIfUnnamedUnsafe();

            result.Sheets.Should().HaveCount(2);
            result.Sheets.First().Name.Should().Be(Some(firstSheetName));
            result.Sheets.Skip(1).First().Name.Should().Be(Some("Sheet2"));
        }

        [Fact]
        public void NameSheetsIfUnnamed_2UnnamedSheets_2DefaultNamedSheets()
        {
            var book = Make.Book(Make.Sheet(), Make.Sheet()).Get;

            var result = book.NameSheetsIfUnnamedUnsafe();

            result.Sheets.Bind(x => x.Name).Should().Equal("Sheet1", "Sheet2");
        }
    }
}