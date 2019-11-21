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
                border: Make.Border(BorderParts.All).Get,
                assert: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.Thin,
                    right: ExcelBorderStyle.Thin,
                    bottom: ExcelBorderStyle.Thin,
                    left: ExcelBorderStyle.Thin));

        [Fact]
        public void Render_Outside_OutsideOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderParts.Outside).Get,
                assertR1C1: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.Thin,
                    right: ExcelBorderStyle.None,
                    bottom: ExcelBorderStyle.None,
                    left: ExcelBorderStyle.Thin),
                assertR1C2: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.Thin,
                    right: ExcelBorderStyle.Thin,
                    bottom: ExcelBorderStyle.None,
                    left: ExcelBorderStyle.None),
                assertR2C2: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.None,
                    right: ExcelBorderStyle.Thin,
                    bottom: ExcelBorderStyle.Thin,
                    left: ExcelBorderStyle.None),
                assertR2C1: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.None,
                    right: ExcelBorderStyle.None,
                    bottom: ExcelBorderStyle.Thin,
                    left: ExcelBorderStyle.Thin));

        [Fact]
        public void Render_Inside_InsideOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderParts.Inside).Get,
                assertR1C1: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.None,
                    right: ExcelBorderStyle.Thin,
                    bottom: ExcelBorderStyle.Thin,
                    left: ExcelBorderStyle.None),
                assertR1C2: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.None,
                    right: ExcelBorderStyle.None,
                    bottom: ExcelBorderStyle.Thin,
                    left: ExcelBorderStyle.Thin),
                assertR2C2: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.Thin,
                    right: ExcelBorderStyle.None,
                    bottom: ExcelBorderStyle.None,
                    left: ExcelBorderStyle.Thin),
                assertR2C1: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.Thin,
                    right: ExcelBorderStyle.Thin,
                    bottom: ExcelBorderStyle.None,
                    left: ExcelBorderStyle.None));

        [Fact]
        public void Render_Horizontal_HorizontalOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderParts.Horizontal).Get,
                assert: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.Thin,
                    right: ExcelBorderStyle.None,
                    bottom: ExcelBorderStyle.Thin,
                    left: ExcelBorderStyle.None));

        [Fact]
        public void Render_Vertical_VerticalOnly() =>
            CheckOn2X2(
                border: Make.Border(BorderParts.Vertical).Get,
                assert: border => border.BorderShouldBe(
                    top: ExcelBorderStyle.None,
                    right: ExcelBorderStyle.Thin,
                    bottom: ExcelBorderStyle.None,
                    left: ExcelBorderStyle.Thin));

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