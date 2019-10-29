namespace BookFx.Tests.EpplusExt
{
    using System.Drawing;
    using BookFx.Epplus;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    [Properties(MaxTest = 10)]
    public class ColorExtTests
    {
        [Property(Arbitrary = new[] { typeof(ColorArb) })]
        public void ToHex_SomeExcelColor_SameAsColorToHex(Color color) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelColor = excelSheet.Cells[1, 1].Style.Font.Color;
                excelColor.SetColor(color);

                var result = excelColor.ToHex();

                result.Should().Be(color.ToHex());
            });

        [Fact]
        public void ToHex_NoExcelColor_IsEmpty() =>
            Packer.OnSheet(excelSheet =>
            {
                var excelColor = excelSheet.Cells[1, 1].Style.Font.Color;

                var result = excelColor.ToHex();

                result.Should().BeEmpty();
            });

        [Property(Arbitrary = new[] { typeof(ColorArb) })]
        public void ToHex_FromHtml_Same(Color color) =>
            ColorTranslator.FromHtml($"#{color.ToHex()}").Should().Be(color);
    }
}