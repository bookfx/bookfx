namespace BookFx.Tests.Renders
{
    using FluentAssertions;
    using OfficeOpenXml.Style;

    public static class BorderExt
    {
        public static void BorderShouldBe(
            this Border border,
            ExcelBorderStyle top,
            ExcelBorderStyle right,
            ExcelBorderStyle bottom,
            ExcelBorderStyle left)
        {
            border.Top.Style.Should().Be(top);
            border.Right.Style.Should().Be(right);
            border.Bottom.Style.Should().Be(bottom);
            border.Left.Style.Should().Be(left);
        }
    }
}