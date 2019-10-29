namespace BookFx.Tests.Renders
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;

    public class BoxProtoRenderTests
    {
        [Fact]
        public void ProtoRender_WithoutSlots_Copied() =>
            Packer.OnSheet(excelSheet => Make
                .Value("A")
                .ToSheet()
                .ToBook()
                .ToBytes()
                .Unpack(protoPackage =>
                {
                    var protoRange = protoPackage.Worksheets[0].Cells[1, 1];
                    var box = BoxCore
                        .Create(BoxType.Proto)
                        .With(proto: ProtoCore
                            .Create(new byte[0], "Ref")
                            .With(range: protoRange));
                    var excelRange = excelSheet.Cells[1, 1];

                    box.ProtoRender()(excelRange);

                    excelRange.Value.Should().Be("A");
                }));

        [Fact]
        public void ProtoRender_WithoutSlots_ColSizeCopied() =>
            Packer.OnSheet(excelSheet => Make
                .Value()
                .SizeCols(100)
                .ToSheet()
                .ToBook()
                .ToBytes()
                .Unpack(protoPackage =>
                {
                    var protoRange = protoPackage.Worksheets[0].Cells[1, 1];
                    var box = BoxCore
                        .Create(BoxType.Proto)
                        .With(proto: ProtoCore
                            .Create(new byte[0], "Ref")
                            .With(range: protoRange));
                    var excelRange = excelSheet.Cells[1, 1];

                    box.ProtoRender()(excelRange);

                    excelRange.Worksheet.Column(1).Width.Should().Be(protoRange.Worksheet.Column(1).Width);
                }));

        [Fact]
        public void ProtoRender_WithoutSlots_RowSizeCopied() =>
            Packer.OnSheet(excelSheet => Make
                .Value()
                .SizeRows(50)
                .ToSheet()
                .ToBook()
                .ToBytes()
                .Unpack(protoPackage =>
                {
                    var protoRange = protoPackage.Worksheets[0].Cells[1, 1];
                    var box = BoxCore
                        .Create(BoxType.Proto)
                        .With(proto: ProtoCore
                            .Create(new byte[0], "Ref")
                            .With(range: protoRange));
                    var excelRange = excelSheet.Cells[1, 1];

                    box.ProtoRender()(excelRange);

                    excelRange.Worksheet.Row(1).Height.Should().Be(protoRange.Worksheet.Row(1).Height);
                }));
    }
}