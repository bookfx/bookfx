namespace BookFx.Tests.Renders
{
    using System;
    using System.Drawing;
    using BookFx.Epplus;
    using BookFx.Renders;
    using BookFx.Tests.Arbitraries;
    using BookFx.Validation;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using Xunit;

    [Properties(MaxTest = 10)]
    public class StyleRenderTests
    {
        [Fact]
        public void Render_Empty_DefaultStyle() =>
            Check(
                Make.Style(),
                range =>
                {
                    range.Style.Font.Size.Should().Be(11);
                    range.Style.Font.Name.Should().Be("Calibri");
                    range.Style.Font.Color.ToHex().Should().BeEmpty();
                    range.Style.Fill.BackgroundColor.ToHex().Should().BeEmpty();
                    range.Style.Font.Bold.Should().BeFalse();
                    range.Style.Font.Italic.Should().BeFalse();
                    range.Style.Font.UnderLine.Should().BeFalse();
                    range.Style.Font.Strike.Should().BeFalse();
                    range.Style.HorizontalAlignment.Should().Be(ExcelHorizontalAlignment.General);
                    range.Style.VerticalAlignment.Should().Be(ExcelVerticalAlignment.Bottom);
                    range.Style.WrapText.Should().BeFalse();
                    range.Style.Indent.Should().Be(0);
                    range.Style.Numberformat.Format.Should().Be("General");
                });

        /// <summary>
        /// <seealso cref="StyleValidator.FontSize"/>
        /// </summary>
        [Property(Arbitrary = new[] { typeof(ValidFontSizeArb) })]
        public void Render_FontSize_Set(double size) =>
            Check(
                Make.Style().Font(size),
                range => range.Style.Font.Size.Should().Be((float)size));

        /// <summary>
        /// <seealso cref="StyleValidator.FontName"/>
        /// </summary>
        [Property]
        public void Render_FontName_Set(NonEmptyString name) =>
            Check(
                Make.Style().Font(name.Get),
                range => range.Style.Font.Name.Should().Be(name.Get));

        [Property(Arbitrary = new[] { typeof(ColorArb) })]
        public void Render_FontColor_Set(Color color) =>
            Check(
                Make.Style().Font(color),
                range => range.Style.Font.Color.ToHex().Should().Be(color.ToHex()));

        [Property(Arbitrary = new[] { typeof(ColorArb) })]
        public void Render_BackColor_Set(Color color) =>
            Check(
                Make.Style().Back(color),
                range => range.Style.Fill.BackgroundColor.ToHex().Should().Be(color.ToHex()));

        [Property]
        public void Render_Bold_Set(bool bold) =>
            Check(
                Make.Style().Bold(bold),
                range => range.Style.Font.Bold.Should().Be(bold));

        [Property]
        public void Render_Italic_Set(bool italic) =>
            Check(
                Make.Style().Italic(italic),
                range => range.Style.Font.Italic.Should().Be(italic));

        [Property]
        public void Render_Underline_Set(bool underline) =>
            Check(
                Make.Style().Underline(underline),
                range => range.Style.Font.UnderLine.Should().Be(underline));

        [Property]
        public void Render_Strike_Set(bool strike) =>
            Check(
                Make.Style().Strike(strike),
                range => range.Style.Font.Strike.Should().Be(strike));

        [Property]
        public void Render_AlignHorizontally_Set(HAlign align) =>
            Check(
                Make.Style().Align(align),
                range => range.Style.HorizontalAlignment.Should().Be((ExcelHorizontalAlignment)align));

        [Property]
        public void Render_AlignVertically_Set(VAlign align) =>
            Check(
                Make.Style().Align(align),
                range => range.Style.VerticalAlignment.Should().Be((ExcelVerticalAlignment)align));

        [Property]
        public void Render_Wrap_Set(bool wrap) =>
            Check(
                Make.Style().Wrap(wrap),
                range => range.Style.WrapText.Should().Be(wrap));

        [Property(Arbitrary = new[] { typeof(ValidRotationArb) })]
        public void Render_Rotation_Set(int rotation) =>
            Check(
                rotation <= 90
                    ? Make.Style().RotateCounterclockwise(rotation)
                    : Make.Style().RotateClockwise(rotation - 90),
                range => range.Style.TextRotation.Should().Be(rotation));

        /// <summary>
        /// <seealso cref="StyleValidator.IndentSize"/>
        /// </summary>
        [Property(Arbitrary = new[] { typeof(ValidIndentSizeArb) })]
        public void Render_Indent_Set(int size) =>
            Check(
                Make.Style().Indent(size),
                range => range.Style.Indent.Should().Be(size));

        /// <summary>
        /// <seealso cref="StyleValidator.Format"/>
        /// </summary>
        [Property]
        public void Render_Format_Set(NonEmptyString format) =>
            Check(
                Make.Style().Format(format.Get),
                range => range.Style.Numberformat.Format.Should().Be(format.Get));

        private static void Check(BoxStyle style, Action<ExcelRange> assertion) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[1, 1];

                style.Get.Render()(excelRange);

                assertion(excelRange);
            });
    }
}