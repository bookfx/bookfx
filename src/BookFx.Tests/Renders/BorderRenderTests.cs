namespace BookFx.Tests.Renders
{
    using System;
    using System.Drawing;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
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
            CheckOn2X2(
                border: Make.Border(BorderPart.All).Get,
                assert: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                });

        [Fact]
        public void Render_Outside_OutsideOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderPart.Outside).Get,
                assertR1C1: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Right.Style.Should().Be(ExcelBorderStyle.None);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                    border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                },
                assertR1C2: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                    border.Left.Style.Should().Be(ExcelBorderStyle.None);
                },
                assertR2C2: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.None);
                    border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Left.Style.Should().Be(ExcelBorderStyle.None);
                },
                assertR2C1: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.None);
                    border.Right.Style.Should().Be(ExcelBorderStyle.None);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                });

        [Fact]
        public void Render_Inside_InsideOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderPart.Inside).Get,
                assertR1C1: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.None);
                    border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Left.Style.Should().Be(ExcelBorderStyle.None);
                },
                assertR1C2: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.None);
                    border.Right.Style.Should().Be(ExcelBorderStyle.None);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                },
                assertR2C2: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Right.Style.Should().Be(ExcelBorderStyle.None);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                    border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                },
                assertR2C1: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                    border.Left.Style.Should().Be(ExcelBorderStyle.None);
                });

        [Fact]
        public void Render_Horizontal_HorizontalOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderPart.Horizontal).Get,
                assert: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Right.Style.Should().Be(ExcelBorderStyle.None);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Left.Style.Should().Be(ExcelBorderStyle.None);
                });

        [Fact]
        public void Render_Vertical_VerticalOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderPart.Vertical).Get,
                assert: border =>
                {
                    border.Top.Style.Should().Be(ExcelBorderStyle.None);
                    border.Right.Style.Should().Be(ExcelBorderStyle.Thin);
                    border.Bottom.Style.Should().Be(ExcelBorderStyle.None);
                    border.Left.Style.Should().Be(ExcelBorderStyle.Thin);
                });

        [Fact]
        public void Render_AllNoColor_NoColor() =>
            CheckOn2X2(
                border: Make.Border().Get,
                assert: border => border.Top.Color.ToHex().Should().BeEmpty());

        [Property(Arbitrary = new[] { typeof(ColorArb) })]
        public void Render_AllRed_AllRed(Color color) =>
            CheckOn2X2(
                border: Make.Border().Color(color).Get,
                assert: border => border.Top.Color.ToHex().Should().Be(color.ToHex()));

        private static void CheckOn2X2(BoxBorderCore border, Action<Border> assert) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                excelRange.GetCells().Map(x => x.Style.Border).ForEach(assert);
            });

        private static void CheckOn2X2(
            BoxBorderCore border,
            Action<Border> assertR1C1,
            Action<Border> assertR1C2,
            Action<Border> assertR2C2,
            Action<Border> assertR2C1) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                border.Render()(excelRange);

                assertR1C1(excelSheet.Cells[Row: 1, Col: 1].Style.Border);
                assertR1C2(excelSheet.Cells[Row: 1, Col: 2].Style.Border);
                assertR2C2(excelSheet.Cells[Row: 2, Col: 2].Style.Border);
                assertR2C1(excelSheet.Cells[Row: 2, Col: 1].Style.Border);
            });
    }
}