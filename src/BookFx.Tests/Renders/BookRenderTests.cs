namespace BookFx.Tests.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;

    public class BookRenderTests
    {
        private const string TheRangeName = "RangeName";

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

        [Fact]
        public void Render_ProtoSheet_NameCopiedInBookScope() =>
            Packer.OnPackage(package => Make
                .Value()
                .NameGlobally(TheRangeName)
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

                    var name = package.Workbook.Names.Single();
                    name.Name.Should().Be(TheRangeName);
                    name.FullAddress.Should().Be("'Sheet name'!A1");
                    package.Workbook.Worksheets[sheetName].Names.Map(x => x.Name).Should().NotContain(TheRangeName);
                }));

        [Fact]
        public void Render_ProtoSheetAndNameExists_NameCopiedInSheetScope() =>
            Packer.OnPackage(package => Make
                .Value()
                .NameGlobally(TheRangeName)
                .ToSheet()
                .ToBook()
                .ToBytes()
                .Unpack(protoPackage =>
                {
                    var existingWorksheet = package.Workbook.Worksheets.Add("Existing Sheet");
                    package.Workbook.Names.Add(TheRangeName, existingWorksheet.Cells[1, 1]);

                    const string sheetName = "Sheet name";
                    var book = BookCore
                        .Create()
                        .Add(SheetCore.Create().With(name: sheetName, protoSheet: protoPackage.Worksheets[0]));

                    book.Render()(package);

                    package.Workbook.Worksheets[sheetName].Names.Map(x => x.Name).Should().Contain(TheRangeName);
                }));
    }
}