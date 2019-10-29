namespace BookFx.Tests.Renders
{
    using System.Drawing;
    using BookFx.Epplus;
    using BookFx.Renders;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck.Xunit;
    using OfficeOpenXml.Style;
    using Xunit;

    [Properties(MaxTest = 10)]
    public class BorderRenderTests
    {
        [Fact]
        public void Render_All_All() =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border(BorderPart.All).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                foreach (var cell in excelRange.GetCells())
                {
                    cell.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    cell.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    cell.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    cell.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                }
            });

        [Fact]
        public void Render_Outside_OutsideOnly() =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border(BorderPart.Outside).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                var cellR1C1 = excelSheet.Cells[Row: 1, Col: 1];
                cellR1C1.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR1C1.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.None);
                cellR1C1.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                cellR1C1.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.Thin);

                var cellR1C2 = excelSheet.Cells[Row: 1, Col: 2];
                cellR1C2.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR1C2.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR1C2.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                cellR1C2.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.None);

                var cellR2C2 = excelSheet.Cells[Row: 2, Col: 2];
                cellR2C2.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.None);
                cellR2C2.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR2C2.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR2C2.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.None);

                var cellR2C1 = excelSheet.Cells[Row: 2, Col: 1];
                cellR2C1.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.None);
                cellR2C1.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.None);
                cellR2C1.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR2C1.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
            });

        [Fact]
        public void Render_Inside_InsideOnly() =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border(BorderPart.Inside).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                var cellR1C1 = excelSheet.Cells[Row: 1, Col: 1];
                cellR1C1.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.None);
                cellR1C1.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR1C1.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR1C1.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.None);

                var cellR1C2 = excelSheet.Cells[Row: 1, Col: 2];
                cellR1C2.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.None);
                cellR1C2.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.None);
                cellR1C2.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR1C2.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.Thin);

                var cellR2C2 = excelSheet.Cells[Row: 2, Col: 2];
                cellR2C2.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR2C2.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.None);
                cellR2C2.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                cellR2C2.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.Thin);

                var cellR2C1 = excelSheet.Cells[Row: 2, Col: 1];
                cellR2C1.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR2C1.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                cellR2C1.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                cellR2C1.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.None);
            });

        [Fact]
        public void Render_Horizontal_HorizontalOnly() =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border(BorderPart.Horizontal).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                foreach (var cell in excelRange.GetCells())
                {
                    cell.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    cell.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.None);
                    cell.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    cell.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.None);
                }
            });

        [Fact]
        public void Render_Vertical_VerticalOnly() =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border(BorderPart.Vertical).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                foreach (var cell in excelRange.GetCells())
                {
                    cell.Style.Border.Top.Style.Should().Be(ExcelBorderStyle.None);
                    cell.Style.Border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    cell.Style.Border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                    cell.Style.Border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                }
            });

        [Fact]
        public void Render_AllNoColor_NoColor() =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border().Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                foreach (var cell in excelRange.GetCells())
                {
                    cell.Style.Border.Top.Color.ToHex().Should().BeEmpty();
                }
            });

        [Property(Arbitrary = new[] { typeof(ColorArb) })]
        public void Render_AllRed_AllRed(Color color) =>
            Packer.OnSheet(excelSheet =>
            {
                var border = Make.Border().Color(color).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                foreach (var cell in excelRange.GetCells())
                {
                    cell.Style.Border.Top.Color.ToHex().Should().Be(color.ToHex());
                }
            });
    }
}