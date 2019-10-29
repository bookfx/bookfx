namespace BookFx.Tests
{
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class BookTests
    {
        [Fact]
        public void Empty_Always_Empty()
        {
            Book.Empty.Should().BeSameAs(Book.Empty);
        }

        [Fact]
        public void Empty_Always_NoSheets()
        {
            Book.Empty.Get.Sheets.Should().BeEmpty();
        }

        [Fact]
        public void Create_Always_Empty()
        {
            Make.Book().Should().BeSameAs(Book.Empty);
        }

        [Fact]
        public void Add_OneSheet_OneSheet()
        {
            Make.Book().Add(Make.Sheet()).Get.Sheets.Should().HaveCount(1);
        }

        [Fact]
        public void Add_3Sheets_3Sheets()
        {
            Make.Book().Add(Enumerable.Repeat(Make.Sheet(), 3)).Get.Sheets.Should().HaveCount(3);
        }
    }
}