namespace BookFx.Tests.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;

    public class BookRenderTests
    {
        [Fact]
        public void Render_ProtoSheet_Copied() =>
            Packer.OnPackage(package => Make
                .Value("A")
                .ToSheet()
                .ToBook()
                .ToBytes()
                .Unpack(protoPackage =>
                {
                    const string sheetName = "Sheet name";
                    var book = BookCore
                        .Create()
                        .Add(SheetCore.Create().With(name: sheetName, protoSheet: protoPackage.Worksheets[0]));

                    book.Render()(package);

                    package.Workbook.Worksheets.Single().Name.Should().Be(sheetName);
                    package.Workbook.Worksheets.Single().Cells[1, 1].Value.Should().Be("A");
                }));
    }
}