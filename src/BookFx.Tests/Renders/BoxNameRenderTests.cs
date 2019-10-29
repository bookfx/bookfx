namespace BookFx.Tests.Renders
{
    using System;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using FsCheck.Xunit;
    using OfficeOpenXml;
    using Xunit;

    [Properties(MaxTest = 10)]
    public class BoxNameRenderTests
    {
        private const string TheValidName = "TheName";

        [Fact]
        public void NameRender_NoName_NotSet() =>
            Check(
                Make.Value().Get,
                range => range.Worksheet.Workbook.Names.Should().BeEmpty());

        [Fact]
        public void NameRender_NonEmptyName_Set() =>
            Check(
                Make.Value().Name(TheValidName).Get,
                range => range.Worksheet.Workbook.Names
                    .Should()
                    .HaveCount(1)
                    .And.OnlyContain(x => x.Name == TheValidName && x.Address == range.Address));

        [Fact]
        public void NameRender_NameIsAlreadyExists_NameIsRedefined() =>
            Packer.OnSheet(excelSheet =>
            {
                excelSheet.Workbook.Names.Add(TheValidName, excelSheet.Cells[2, 2]);
                var box = Make.Value().Name(TheValidName).Get;
                var excelRange = excelSheet.Cells[1, 1];

                box.NameRender()(excelRange);

                excelSheet.Workbook.Names.Should()
                    .HaveCount(1)
                    .And.OnlyContain(x => x.Name == TheValidName && x.Address == excelRange.Address);
            });

        private static void Check(BoxCore box, Action<ExcelRange> assertion) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[1, 1];

                box.NameRender()(excelRange);

                assertion(excelRange);
            });
    }
}