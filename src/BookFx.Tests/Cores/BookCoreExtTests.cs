namespace BookFx.Tests.Cores
{
    using BookFx.Cores;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class BookCoreExtTests
    {
        [Fact]
        public void ProtoBooks_ProtoSheet_Contains()
        {
            var protoBook = new byte[0];
            var book = Make.Sheet(protoBook).ToBook().Get;

            var result = book.ProtoBooks();

            result.Should().Equal(List(protoBook));
        }

        [Fact]
        public void ProtoBooks_ProtoBox_Contains()
        {
            var protoBook = new byte[0];
            var book = Make.Proto(protoBook, "AReference").ToSheet().ToBook().Get;

            var result = book.ProtoBooks();

            result.Should().Equal(List(protoBook));
        }

        [Fact]
        public void ProtoBooks_OneProtoBookInTwoProtoBoxes_ContainsOne()
        {
            var protoBook = new byte[0];
            var book = Make
                .Row()
                .Add(Make.Proto(protoBook, "AReference"))
                .Add(Make.Proto(protoBook, "AReference"))
                .ToSheet()
                .ToBook()
                .Get;

            var result = book.ProtoBooks();

            result.Should().Equal(List(protoBook));
        }
    }
}